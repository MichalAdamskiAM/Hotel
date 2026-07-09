using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class RoomSearchingDTO
    {
        public DateOnly? StartDate { get; set; } = null;
        public DateOnly? EndDate { get; set; } = null;

        public ICollection<int> AmenityIds { get; set; } = [];

        [Range(1, 30)]
        public required int NumberOfPeople { get; set; } = 1;

        public required decimal Area { get; set; } = 0;

        public required decimal Price { get; set; } = decimal.MaxValue;

        [StringLength(1000)]
        public string Description { get; set; } = "";
    }
}
