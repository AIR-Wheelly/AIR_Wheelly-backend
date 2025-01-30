using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AIR_Wheelly_API.Hubs;
[Authorize]
public class NotificationHub : Hub
{
    private readonly ICarService _carService;
    public NotificationHub(ICarService carService)
    {
        _carService = carService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst("id")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst("id")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnDisconnectedAsync(exception);
    }
    public async Task NotifyOwner(Guid reservationId)
    {
        try
        {
            var reservations = await _carService.GetCarReservationsForOwner(reservationId);

            if (reservations == null || !reservations.Any())
            {
                throw new InvalidOperationException($"No reservations found for reservation ID {reservationId}.");
            }

            foreach (var reservation in reservations)
            {
                var notificationMessage = new
                {
                    ReservationId = reservation.Id,
                    Message = $"A new reservation has been made for car  {reservation.CarListingId}.",
                    TotalPrice = reservation.TotalPrice,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate
                };

                await Clients.User(reservation.UserId.ToString()).SendAsync("NotifyOwner", notificationMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification for Reservation ID {reservationId}: {ex.Message}");
            throw;
        }
    }
}