using System.Collections.Generic;

namespace Hotel.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool DarkMode { get; set; } = false;

        public int RoleId { get; set; } = 1;
        public Role Role { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}