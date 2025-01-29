using AIR_Wheelly_Common.Enums;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.DTO.Response;

public class CarReservationResponse
{
    public Guid Id { get; set; }
    public Guid CarListingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
    public bool IsPaid { get; set; }
    public CarListing CarListing { get; set; }


}