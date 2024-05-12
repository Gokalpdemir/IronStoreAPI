
using ETicaretAPI.Infrastructure.Services.Storage;
using ETıcaretAPI.Application.Abstractions.Storage;
using ETıcaretAPI.Infrastructure.Services;
using ETıcaretAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Infrastructure.Services.Token;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Abstractions.Services.Configurations;
using ETıcaretAPI.Infrastructure.Services.Configurations;

namespace ETıcaretAPI.Infrastructure.Extension
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped <IMailService, MailService>();
            services.AddScoped <IApplicationService, ApplicationService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T :Storage,IStorage
        {
            services.AddScoped<IStorage,T>();
        }
    }
}
