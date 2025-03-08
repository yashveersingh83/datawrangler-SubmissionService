using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Settings;
using SubmissionService.Application.Features.Cache.Query;
using System.Reflection;

namespace SubmissionService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services
            //services.AddRedisCache();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            //services.AddMediator(typeof(CacheWarmUpQueryHandler)); // Register MediatR

            
            return services;
        }
    }

}
