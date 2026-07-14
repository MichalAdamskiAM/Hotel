using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotelBackend.Authorization
{
    public interface IPrivilegeRequirement : IAuthorizationRequirement
    {
        public bool IsFulfilled(List<Claim> userPrivileges);
    }
}