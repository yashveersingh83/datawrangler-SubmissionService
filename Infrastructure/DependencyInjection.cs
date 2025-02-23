using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.MongoDB;
using SharedKernel.Settings;
using SubmissionService.Domain;
using System.Reflection;

namespace SubmissionService.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
    {
        // Register services
        services.AddAutoMapper(typeof(AutoMapperProfile));
        //services.AddSingleton<IMileStoneService, MileStoneService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

        // services.AddKeycloakWebApiAuthentication(_configuration);
        // services.AddAuthorization();

          services.AddMongo()
                  .AddMongoRepository<MileStone>("Milestone")
                  .AddMongoRepository<OrganizationalUnitHead>("OrganizationalUnitHead")
                  .AddMongoRepository<Recipient>("Recipient")
                  .AddMongoRepository<InformationRequest>("InformationRequest");



        //.AddMassTransitWithRabbitMq();

        return services;
    }
}

