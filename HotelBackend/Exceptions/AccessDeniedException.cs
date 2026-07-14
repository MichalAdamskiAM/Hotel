using HotelBackend.Authorization;

namespace HotelBackend.Exceptions
{
    public class AccessDeniedException : Exception
    {
        private readonly IPrivilegeRequirement? unfulfilledRequirement;

        public AccessDeniedException() : base("User doesn't have access to the resource.") { }

        public AccessDeniedException(string message) : base(message) { }

        public AccessDeniedException(IPrivilegeRequirement? unfulfilledRequirement) : base("User doesn't have access to the resource.")
        {
            this.unfulfilledRequirement = unfulfilledRequirement;
        }
    }
}