namespace Hotel.DTOs
{
    public class GettingRoomDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public List<string> AmenityNames { get; set; } = [];
    }
}
