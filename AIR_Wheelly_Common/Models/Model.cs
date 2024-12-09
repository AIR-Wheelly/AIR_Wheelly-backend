namespace AIR_Wheelly_Common.Models;

public class Model
{
    public Guid Id { get; set; }
    public Guid ManafacturerId { get; set; }
    public string Name { get; set; }
    
    public Manafacturer Manafacturer { get; set; }
    public ICollection<CarListing> CarListings { get; set; }
}