using System.Text.Json.Serialization;

namespace AIR_Wheelly_Common.Models;

public class CarListingPicture
{
    public Guid Id { get; set; }
    public Guid CarListingId { get; set; }
    public string Image { get; set; }

    [JsonIgnore]
    public CarListing CarListing { get; set; }
    
}