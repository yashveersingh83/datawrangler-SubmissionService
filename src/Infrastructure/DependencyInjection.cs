using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using SharedKernel.MongoDB;
using SharedKernel.Settings;
using StackExchange.Redis;
using SubmissionService.Application;
using SubmissionService.Application.Features.Cache.Query;
using SubmissionService.Domain;
using System.Reflection;

namespace SubmissionService.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
    {

        try
        {

            services.AddRedisCache(configuration);
        }
        catch { }
        // Register services
        services.AddAutoMapper(typeof(AutoMapperProfile));
        //services.AddSingleton<IMileStoneService, MileStoneService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
        
        
        // services.AddKeycloakWebApiAuthentication(_configuration);
        // services.AddAuthorization();

        services.AddMongo()

                  .AddMongoRepository<MileStone>("MileStone")
                  .AddMongoRepository<RequestStatus>("RequestStatus")                  
                  .AddMongoRepository<OrganizationalUnitHead>("OrganizationalUnitHead")
                  .AddMongoRepository<OrganizationalUnit>("OrgUnit")
                  .AddMongoRepository<Recipient>("Recipient")
                  .AddMongoRepository<InformationRequest>("InformationRequest");


       
        //.AddMassTransitWithRabbitMq();

        return services;
    }
}

