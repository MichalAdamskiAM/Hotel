using HotelBackend.DTOs;

namespace HotelBackend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int NumberOfPeople { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<RoomAmenity> RoomAmenities { get; set; } = [];

        public ICollection<RoomReservation> RoomReservations { get; set; } = [];

        public int MatchScore(RoomSearchingDTO search)
        {
            if (NumberOfPeople < search.MinNumberOfPeople ||
                Area < search.MinArea || Price > search.MaxPrice)
            {
                return 0;
            }

            return
                search.AmenityIds.Count(id => RoomAmenities.Any(ra => ra.AmenityId == id)) +
                search.Keywords.Count(k => Description.Contains(k));
        }

        public List<int> MissingAmenityIds(RoomSearchingDTO search)
        {
            var result = new List<int>();
            foreach (var amenityId in search.AmenityIds)
            {
                if (!this.RoomAmenities.Select(ra => ra.AmenityId).Contains(amenityId))
                {
                    result.Add(amenityId);
                }
            }
            return result;
        }

        public List<string> MissingKeywords(RoomSearchingDTO search)
        {
            var result = new List<string>();
            foreach (var keyword in search.Keywords)
            {
                if (!this.Description.Contains(keyword))
                {
                    result.Add(keyword);
                }
            }
            return result;
        }
    }
}