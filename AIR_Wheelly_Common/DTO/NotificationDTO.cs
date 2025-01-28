namespace AIR_Wheelly_Common.DTO;

public class NotificationDTO
{
    public Guid NotificationId { get; set; } = Guid.NewGuid();
    public Guid OwnerId { get; set; } 
    public Guid RenterId { get; set; }
    public string Message { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}