namespace SubmissionService.API
{
    using MassTransit;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Prometheus;
    using SharedKernel.MassTransit;

    //using SharedKernel.Settings;
    using SubmissionService.API.Security;
    using System.Security.Claims;
    using System.Text.Json;

    public class Startup
    {
        private const string AnalystPolicy = "AnalystOnly";
        private const string CoordinatoryPolicy = "CoordinatorOnly";
        private const string ApproverPolicy = "ApproverOnly";
        public IConfiguration _configuration;
        // private ServiceSettings serviceSettings;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ConfigureServices method for registering services
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.AddHttpClient();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            AddSwaggerSecurityScheame(services);
            services.AddMassTransitWithRabbitMq();
            // services.AddAuthorization();

            AddAuthorizationPolicies(services);

            AddKeycloakJwtAuthentication(services);

            services.AddSingleton<IAuthorizationHandler, KeycloakRoleHandler>();
        }

        private void AddKeycloakJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Audience = _configuration["Authentication:Audience"];
                    //o.MetadataAddress = _configuration["Authentication:MetadataAddress"];
                    o.Authority = _configuration["Authentication:ValidIssuer"];
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = _configuration["Authentication:ValidIssuer"],
                        ValidateIssuerSigningKey = true, 
                        ValidateAudience = true, 
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

                            // Map Keycloak realm roles
                            var realmRoles = context.Principal.FindFirst("realm_access")?.Value;
                            if (realmRoles != null)
                            {
                                var parsed = JsonDocument.Parse(realmRoles);
                                foreach (var role in parsed.RootElement.GetProperty("roles").EnumerateArray())
                                {
                                    claimsIdentity.AddClaim(new Claim("role", role.GetString()));
                                }
                            }

                            // Map Keycloak client roles
                            var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;
                            if (resourceAccess != null)
                            {
                                var parsed = JsonDocument.Parse(resourceAccess);
                                if (parsed.RootElement.TryGetProperty("your-client-id", out var client))
                                {
                                    foreach (var role in client.GetProperty("roles").EnumerateArray())
                                    {
                                        claimsIdentity.AddClaim(new Claim("role", role.GetString()));
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                }
                );
        }

        private static void AddAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {

                options.AddPolicy(AnalystPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Analyst")));

                options.AddPolicy(CoordinatoryPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Coordinator")));

                options.AddPolicy(ApproverPolicy, policy =>
                    policy.Requirements.Add(new KeycloakRoleRequirement("Approver")));

                options.AddPolicy("AnyAllowedUser", policy =>
                 policy.Requirements.Add(new KeycloakRoleRequirement("Analyst", "Coordinator", "Approver")));
            });
        }

        private void AddSwaggerSecurityScheame(IServiceCollection services)
        {
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
        }

        // Configure method for setting up middleware
        public void Configure(WebApplication app)
        {
            app.UseMetricServer();
            app.UseHttpMetrics();
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
