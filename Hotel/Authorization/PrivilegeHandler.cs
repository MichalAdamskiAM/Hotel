using Microsoft.AspNetCore.Authorization;

namespace Hotel.Authorization
{
    public class PrivilegeHandler : AuthorizationHandler<IPrivilegeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IPrivilegeRequirement requirement)
        {
            var hasPrivilege = requirement.IsFulfilled([.. context.User.Claims.Where(c => c.Type == "Privilege")]);

            if (hasPrivilege)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}