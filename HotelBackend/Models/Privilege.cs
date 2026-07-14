namespace HotelBackend.Models
{
    public class Privilege
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<RolePrivilege> RolePrivileges { get; set; } = [];
    }
}