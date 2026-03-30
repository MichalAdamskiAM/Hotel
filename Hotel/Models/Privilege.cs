namespace Hotel.Models
{
    public class Privilege
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
    }
}