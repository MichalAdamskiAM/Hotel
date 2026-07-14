using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotelBackend.Authorization
{
    public class PrivilegeRequirements(List<IPrivilegeRequirement> privilegeRequirements) : IPrivilegeRequirement
    {
        public List<IPrivilegeRequirement> PrivilegeRequirementList { get; } = privilegeRequirements;

        public void AddPrivilegeRequirement(IPrivilegeRequirement privilegeRequirement)
        {
            PrivilegeRequirementList.Add(privilegeRequirement);
        }

        public bool IsFulfilled(List<Claim> userPrivileges)
        {
            foreach (var privilegeRequirement in PrivilegeRequirementList)
            {
                if (!privilegeRequirement.IsFulfilled(userPrivileges))
                    return false;
            }
            return true;
        }
    }
}