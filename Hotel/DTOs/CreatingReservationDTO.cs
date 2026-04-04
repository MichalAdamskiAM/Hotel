namespace Hotel.DTOs
{
    public class CreatingReservationDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int UserId { get; set; }
        public ICollection<int> RoomNumbers { get; set; } = null!;
    }
}