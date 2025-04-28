using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace SubmissionService.API.Security
{
    public class KeycloakRoleHandler : AuthorizationHandler<KeycloakRoleRequirement>
    {
        private readonly ILogger<KeycloakRoleHandler> _logger;

        public KeycloakRoleHandler(ILogger<KeycloakRoleHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeycloakRoleRequirement requirement)
        {
            var userRoles = context.User.FindAll("role").Select(c => c.Value).ToList();

            _logger.LogInformation("User has roles: {Roles}", string.Join(", ", userRoles));

            if (requirement.Roles.Any(requiredRole => userRoles.Contains(requiredRole)))
            {
                _logger.LogInformation("Authorization succeeded. At least one required role was found.");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("Authorization failed. None of the required roles were found.");
            }

            return Task.CompletedTask;
        }
    }
}
