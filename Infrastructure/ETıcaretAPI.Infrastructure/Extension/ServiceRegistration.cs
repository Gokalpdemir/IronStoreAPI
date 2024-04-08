
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

namespace ETıcaretAPI.Infrastructure.Extension
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T :Storage,IStorage
        {
            services.AddScoped<IStorage,T>();
        }
    }
}
