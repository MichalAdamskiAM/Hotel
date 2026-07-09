namespace Hotel.Models
{
    public class Status
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}