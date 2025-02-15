namespace SubmissionService.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using SharedKernel.MongoDB;
    using SharedKernel.Settings;
    using SubmissionService.API.Security;
    using SubmissionService.Domain;

    public class Startup
    {
        private const string AnalystPolicy = "AnalystOnly";
        private const string CoordinatoryPolicy = "CoordinatorOnly";
        private const string ApproverPolicy = "ApproverOnly";
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

           // services.AddKeycloakWebApiAuthentication(_configuration);
           // services.AddAuthorization();

            services.AddMongo()
                     .AddMongoRepository<MileStone>("Milestone");
            //.AddMassTransitWithRabbitMq();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()//WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(_configuration["Keycloak:Authorizationurl"]),
                            Scopes = new Dictionary<string, string>
                            {
                                {"openid","openid" },{"profile","profile"}
                            }
                        }
                    }

                });
            
            var securityRequirement = new OpenApiSecurityRequirement 
            {
                {
                    new OpenApiSecurityScheme

                    {
                        Reference = new  OpenApiReference                    
                        {
                                Id="Keycloak" , Type =ReferenceType.SecurityScheme                        
                        },In=ParameterLocation.Header,Name="Bearer" , Scheme="Bearer"


                    },[]
                }
            
            };
                o.AddSecurityRequirement(securityRequirement);
            });
            // services.AddAuthorization();
            services.AddAuthorization(options =>
            {               

                options.AddPolicy(AnalystPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Analyst")));

                options.AddPolicy(CoordinatoryPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Coordinator")));

                options.AddPolicy(ApproverPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Approver")));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Audience = _configuration["Authentication:Audience"];
                    o.MetadataAddress = _configuration["Authentication:MetadataAddress"];

                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = _configuration["Authentication:ValidIssuer"],
                    };

                }
                );

            //services.AddKeycloakAuthorization();
            //services.AddKeycloakAuthentication(
            //    _configuration["Authentication:MetadataAddress"], 
            //    _configuration["Authentication:Audience"], 
            //    _configuration["Authentication:ValidIssuer"]


            //    );
            //    services.AddHttpContextAccessor();



            services.AddSingleton<IAuthorizationHandler, KeycloakRoleHandler>();
        }

        // Configure method for setting up middleware
        public void Configure(WebApplication app)
        {
            // Use Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAnyOrigin");
            // Add other middlewares
            app.UseAuthentication(); 
            app.UseAuthorization();

            // Map controller endpoints
            app.MapControllers();
        }
    }

}
