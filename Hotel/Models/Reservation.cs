using System;

namespace Hotel.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();

    }
}