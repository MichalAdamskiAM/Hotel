using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.Exceptions
{
    public class AccessDeniedException : Exception
    {
        private readonly PrivilegeRequirement? unfulfilledRequirement;

        public AccessDeniedException() : base("User doesn't have access to the resource.") { }

        public AccessDeniedException(string message) : base(message) { }

        public AccessDeniedException(PrivilegeRequirement? unfulfilledRequirement)
        {
            this.unfulfilledRequirement = unfulfilledRequirement;
        }
    }
}