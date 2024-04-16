using ETıcaretAPI.Application.Extension;
using ETıcaretAPI.Application.Validators.Products;
using ETıcaretAPI.Infrastructure.Extension;
using ETıcaretAPI.Infrastructure.Filters;
using ETıcaretAPI.Infrastructure.Services.Storage.Local;
using ETıcaretAPI.Persistence.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ETıcaretAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddAplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddStorage<LocalStorage>();
            builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
            builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();   
            app.UseCors();
            app.UseHttpsRedirection();

           


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
