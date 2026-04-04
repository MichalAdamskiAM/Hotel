namespace Hotel.DTOs
{
    public class CreatingRoomDto
    {
        public int Number { get; set; }

        public bool Balcony { get; set; } = false;
        public bool SeaView { get; set; } = false;
        public bool Refrigerator { get; set; } = false;
        public bool Kettle { get; set; } = false;
        public bool AirConditioning { get; set; } = false;
        public bool Safe { get; set; } = false;
        public bool TV { get; set; } = false;

        public int NumberOfPeople { get; set; }
        public decimal Area { get; set; }
        public string Description { get; set; } = "";
    }
}