using System.ComponentModel.DataAnnotations;

namespace HotelBackend.DTOs
{
    public class RoomSearchingDTO
    {
        public DateOnly? StartDate { get; set; } = null;
        public DateOnly? EndDate { get; set; } = null;

        public ICollection<int> AmenityIds { get; set; } = [];

        [Range(1, 30)]
        public int MinNumberOfPeople { get; set; } = 1;
        public decimal MinArea { get; set; } = 0;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public ICollection<string> Keywords { get; set; } = [];
    }
}