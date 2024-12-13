using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.DTO;

public class CarListingDTO
{
    public Guid ModelId { get; set; }
    public int YearOfProduction { get; set; }
    public int NumberOfSeats { get; set; }
    public string FuelType { get; set; }
    public double RentalPrice { get; set; }
    public Guid LocationId { get; set; }
    public double NumberOfKilometers { get; set; }
    public string RegistrationNumber { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
}