using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class CreatingReservationDto
    {
        [Required]
        public required DateOnly StartDate { get; set; }

        [Required]
        public required DateOnly EndDate { get; set; }

        public required int UserId { get; set; }

        [Required]
        public required ICollection<int> RoomIds { get; set; } = null!;
    }
}