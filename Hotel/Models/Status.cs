using System.Collections.Generic;

namespace Hotel.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}