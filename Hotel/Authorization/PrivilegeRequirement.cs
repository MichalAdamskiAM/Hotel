using Microsoft.AspNetCore.Authorization;

public class PrivilegeRequirement : IAuthorizationRequirement
{
    public string PrivilegeName { get; }

    public PrivilegeRequirement(string privilegeName)
    {
        PrivilegeName = privilegeName;
    }
}