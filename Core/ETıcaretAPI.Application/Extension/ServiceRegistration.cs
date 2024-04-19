using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Extension
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration=> configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
           

        }
    }
}
