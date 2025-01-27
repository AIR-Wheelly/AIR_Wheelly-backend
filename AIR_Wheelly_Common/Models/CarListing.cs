using System.Text.Json.Serialization;

namespace AIR_Wheelly_Common.Models;

public class CarListing
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public int YearOfProduction { get; set; }
    public int NumberOfSeats { get; set; }
    public string FuelType { get; set; }
    public double RentalPriceType {get; set;}
    public Guid LocationId { get; set; }
    public Location Location { get; set; }
    public double NumberOfKilometers { get; set; }
    public string RegistrationNumber { get; set; }
    public string Description {get; set;}
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Model Model { get; set; }
    public ICollection<CarListingPicture> CarListingPictures { get; set; }
    [JsonIgnore]
    public ICollection<CarReservation> CarReservations { get; set; } = new List<CarReservation>();

    public ICollection<Review> Reviews { get; set; } = null!;
}