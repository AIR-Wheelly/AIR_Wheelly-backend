using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AIR_Wheelly_Common.Models;

public class Model
{
    public Guid Id { get; set; }
    public Guid ManafacturerId { get; set; }
    public string Name { get; set; }

    [NotMapped]
    public string? ManafacturerName { get; set; } = null;

    [JsonIgnore]
    public Manafacturer Manafacturer { get; set; }
    [JsonIgnore]
    public ICollection<CarListing> CarListings { get; set; }
}