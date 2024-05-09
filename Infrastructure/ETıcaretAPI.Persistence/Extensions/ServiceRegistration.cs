using ETıcaretAPI.Application.Abstractions.Basket;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Abstractions.Services.Authentication;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities.Identity;
using ETıcaretAPI.Infrastructure.Services.User;
using ETıcaretAPI.Persistence.Contexts;
using ETıcaretAPI.Persistence.Repositories;
using ETıcaretAPI.Persistence.Services;
using ETıcaretAPI.Persistence.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Extensions
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSQL")));
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.User.AllowedUserNameCharacters += "ğĞüÜşŞıİöÖçÇ";
                
            }).AddEntityFrameworkStores<ETicaretAPIDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository,ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();




        }

    }
}
