using System.Text.Json.Serialization;

namespace Hotel.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; } = null!;
        public int RoomId { get; set; }

        [JsonIgnore]
        public Room Room { get; set; } = null!;
    }
}