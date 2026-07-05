namespace Hotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public bool Balcony { get; set; }
        public bool SeaView { get; set; }
        public bool Refrigerator { get; set; }
        public bool Kettle { get; set; }
        public bool AirConditioning { get; set; }
        public bool Safe { get; set; }
        public bool TV { get; set; }

        public int NumberOfPeople { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
    }
}