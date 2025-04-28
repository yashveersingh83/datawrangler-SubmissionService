namespace SubmissionService.API.Security
{
    using Microsoft.AspNetCore.Authorization;

    public class KeycloakRoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Roles { get; }

        public KeycloakRoleRequirement(params string[] roles)
        {
            Roles = roles;
        }
    }


}
