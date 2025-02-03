namespace SubmissionService.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SharedKernel.MongoDB;
    using SharedKernel.Settings;

    public class Startup
    {
        public IConfiguration _configuration;
        private ServiceSettings serviceSettings;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ConfigureServices method for registering services
        public void ConfigureServices(IServiceCollection services)
        {
            serviceSettings = _configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddMongo()
                     .AddMongoRepository<Item>("items");
            //.AddMassTransitWithRabbitMq();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // Configure method for setting up middleware
        public void Configure(WebApplication app)
        {
            // Use Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI();

            // Add other middlewares
            app.UseAuthorization();

            // Map controller endpoints
            app.MapControllers();
        }
    }

}
