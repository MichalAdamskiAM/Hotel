using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class CreatingRoomDto
    {
        [Range(1, 9999)]
        public required int Number { get; set; }

        public required ICollection<int> AmenityIds { get; set; }

        [Range(1, 30)]
        public required int NumberOfPeople { get; set; }

        public required decimal Area { get; set; }

        public required decimal Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = "";
    }
}