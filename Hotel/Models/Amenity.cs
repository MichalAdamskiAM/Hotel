using System.Text.Json.Serialization;

namespace Hotel.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<RoomAmenity> RoomAmenities { get; set; } = [];
    }
}