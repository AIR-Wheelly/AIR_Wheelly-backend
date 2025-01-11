using AIR_Wheelly_Common.Enums;

namespace AIR_Wheelly_Common.Models;

public class CarReservation
{
    public Guid Id { get; set; }
    public Guid CarListingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public RentalStatus Status { get; set; } = RentalStatus.Pending;
    public bool IsPaid { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public CarListing CarListing { get; set; }
    public User User { get; set; }
    
}