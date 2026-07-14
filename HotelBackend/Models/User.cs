namespace HotelBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }

        public bool DarkMode { get; set; } = false;

        public int RoleId { get; set; } = 1;
        public Role Role { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}