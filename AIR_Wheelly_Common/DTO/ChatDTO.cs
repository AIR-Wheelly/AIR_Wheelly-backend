namespace AIR_Wheelly_Common.DTO;

public class ChatDTO
{
    public Guid ReservationId { get; set; }
    public Guid SenderId { get; set; }
    public string Message { get; set; }
}