namespace Hotel.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}