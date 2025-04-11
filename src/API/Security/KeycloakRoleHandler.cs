namespace SubmissionService.API.Security

{
    using Microsoft.AspNetCore.Authorization;
    
    using System.Linq;
    using System.Threading.Tasks;
    public class KeycloakRoleHandler : AuthorizationHandler<KeycloakRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeycloakRoleRequirement requirement)
        {
            var resourceAccessClaim = context.User.Claims.FirstOrDefault(c => c.Type == "resource_access"|| c.Type == "realm_access");

            if (resourceAccessClaim != null && resourceAccessClaim.Value.Contains($"\"{requirement.Role}\""))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
