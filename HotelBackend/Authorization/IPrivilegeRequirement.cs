using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.Authorization
{
    public interface IPrivilegeRequirement : IAuthorizationRequirement
    {
        public bool IsFulfilled(List<Claim> userPrivileges);
    }
}