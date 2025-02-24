﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Settings;
using System.Reflection;

namespace SubmissionService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();



            return services;
        }
    }

}
