using EticaretAPI.SignalR.Extension;
using EticaretAPI.SignalR.Hubs;
using ETıcaretAPI.API.Configurations.ColumnWriters;
using ETıcaretAPI.API.Extensions;
using ETıcaretAPI.Application.Extension;
using ETıcaretAPI.Application.Validators.Products;
using ETıcaretAPI.Infrastructure.Extension;
using ETıcaretAPI.Infrastructure.Filters;
using ETıcaretAPI.Infrastructure.Services.Storage.Local;
using ETıcaretAPI.Persistence.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace ETıcaretAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddHttpContextAccessor(); //requestler neticesinde gelen requestlere karşılık oluşan httpContext nesnesine katmanlardaki classlar üzerinden erişebilmemizi sağlayan servisttir.
            
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddAplicationServices();
            builder.Services.AddSignalRServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddStorage<LocalStorage>();
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            // Serilog
            SqlColumn sqlColumn = new SqlColumn();
            sqlColumn.ColumnName = "UserName";
            sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
            sqlColumn.PropertyName = "UserName";
            sqlColumn.DataLength = 50;
            sqlColumn.AllowNull = true;
            ColumnOptions columnOpt = new ColumnOptions();
            columnOpt.Store.Remove(StandardColumn.Properties);
            columnOpt.Store.Add(StandardColumn.LogEvent);
            columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

            Logger log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("MSSQL"),
                 sinkOptions: new MSSqlServerSinkOptions
                 {
                     AutoCreateSqlTable = true,
                     TableName = "logs",
                 },
                 appConfiguration: null,
                 columnOptions: columnOpt
                )
               .WriteTo.Seq(builder.Configuration["Seq:Server-Url"])
                .Enrich.FromLogContext()
                .Enrich.With<UsernameColumnWriter>()
                .MinimumLevel.Information()
                .CreateLogger();
            builder.Host.UseSerilog(log);

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
            //serilogEnd
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, // oluşturulacak token değerini hangi sitelerin kullanıcağını bildirir
                        ValidateIssuer = true, // oluşturulacak token değerini kimin dağıttığğını bildirir (api)
                        ValidateLifetime = true,// token geçerlilik süresi
                        ValidateIssuerSigningKey = true, // üretilecek olan token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key  verisinin doğrulanmasıdır

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                        NameClaimType = ClaimTypes.Name,
                        

                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfiguureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseHttpLogging();
            app.UseCors();
            app.UseHttpsRedirection();




            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("UserName", username);
                await next();
            });
            app.MapControllers();
            app.AddHubRegistration();
            app.Run();
        }
    }
}
