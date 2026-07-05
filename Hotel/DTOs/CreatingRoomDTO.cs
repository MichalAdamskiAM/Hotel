using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class CreatingRoomDto
    {
        [Range(1, 9999)]
        public required int Number { get; set; }

        public bool Balcony { get; set; } = false;
        public bool SeaView { get; set; } = false;
        public bool Refrigerator { get; set; } = false;
        public bool Kettle { get; set; } = false;
        public bool AirConditioning { get; set; } = false;
        public bool Safe { get; set; } = false;
        public bool TV { get; set; } = false;

        [Range(1, 30)]
        public required int NumberOfPeople { get; set; }

        public required decimal Area { get; set; }

        public required decimal Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = "";
    }
}