using System.Text.Json.Serialization;

namespace Hotel.Models
{
    public class RoomReservation
    {
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public int ReservationId { get; set; }

        [JsonIgnore]
        public Reservation Reservation { get; set; } = null!;
    }
}