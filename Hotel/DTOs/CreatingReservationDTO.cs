namespace Hotel.DTOs
{
    public class CreatingReservationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public ICollection<int> RoomNumbers { get; set; } = null!;
    }
}