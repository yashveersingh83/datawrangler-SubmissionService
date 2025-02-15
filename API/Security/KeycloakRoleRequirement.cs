namespace SubmissionService.API.Security
{
    using Microsoft.AspNetCore.Authorization;

    public class KeycloakRoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public KeycloakRoleRequirement(string role)
        {
            Role = role;
        }
    }


}
