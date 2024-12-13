namespace AIR_Wheelly_Common.Models;

public class Location
{
    public Guid LocationId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Adress { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}