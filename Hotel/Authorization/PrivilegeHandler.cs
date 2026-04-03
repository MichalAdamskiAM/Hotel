using Microsoft.AspNetCore.Authorization;

namespace Hotel.Authorization
{
    public class PrivilegeHandler : AuthorizationHandler<PrivilegeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PrivilegeRequirement requirement)
        {
            var hasPrivilege = context.User.Claims
                .Where(c => c.Type == "Privilege")
                .Any(c => c.Value == requirement.PrivilegeName);

            if (hasPrivilege)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}