using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotelBackend.Authorization
{
    public class PrivilegeRequirement(string privilegeName) : IPrivilegeRequirement
    {
        public string PrivilegeName { get; } = privilegeName;
        
        public bool IsFulfilled(List<Claim> userPrivileges)
        {
            return userPrivileges.Any(up => up.Value == PrivilegeName);
        }
    }
}