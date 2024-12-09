namespace AIR_Wheelly_Common.Models;

public class CarListingPicture
{
    public Guid Id { get; set; }
    public Guid CarListingId { get; set; }
    public string Image { get; set; }
    
    public CarListing CarListing { get; set; }
    
}