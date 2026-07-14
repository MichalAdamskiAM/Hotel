namespace HotelBackend.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public ICollection<RoomReservation> RoomReservations { get; set; } = [];
    }
}