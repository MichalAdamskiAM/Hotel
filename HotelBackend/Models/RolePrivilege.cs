namespace HotelBackend.Models
{
    public class RolePrivilege
    {
        public int PrivilegeId { get; set; }
        public Privilege Privilege { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}