namespace Hotel.Models
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

        public bool HasAmenities(ICollection<int> amenityIds)
        {
            foreach (var amenityId in amenityIds)
            {
                if (!this.RoomAmenities.Select(ra => ra.AmenityId).Contains(amenityId))
                {
                    return false;
                }
            }
            return true;
        }
    }
}