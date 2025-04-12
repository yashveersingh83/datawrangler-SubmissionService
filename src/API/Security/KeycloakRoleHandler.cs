namespace SubmissionService.API.Security

{
    using Microsoft.AspNetCore.Authorization;
    
    using System.Linq;
    using System.Threading.Tasks;

    public class KeycloakRoleHandler : AuthorizationHandler<KeycloakRoleRequirement>
    {
        private readonly ILogger<KeycloakRoleHandler> _logger;

        public KeycloakRoleHandler(ILogger<KeycloakRoleHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeycloakRoleRequirement requirement)
        {
            var resourceAccessClaim = context.User.Claims.FirstOrDefault(c => c.Type == "resource_access" || c.Type == "realm_access");

            if (resourceAccessClaim != null)
            {
                _logger.LogInformation("Found claim: {ClaimType} with value: {ClaimValue}", resourceAccessClaim.Type, resourceAccessClaim.Value);

                if (resourceAccessClaim.Value.Contains($"\"{requirement.Role}\""))
                {
                    _logger.LogInformation("Role {Role} found in the claim. Authorization succeeded.", requirement.Role);
                    context.Succeed(requirement);
                }
                else
                {
                    _logger.LogWarning("Role {Role} not found in the claim. Authorization failed.", requirement.Role);
                }
            }
            else
            {
                _logger.LogWarning("No resource_access or realm_access claim found in the token.");
            }

            return Task.CompletedTask;
        }
    }

}
