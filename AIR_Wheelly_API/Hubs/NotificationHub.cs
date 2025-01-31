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
        Console.WriteLine($"User connected: {Context.ConnectionId}");

        var userId = Context.User?.FindFirst("id")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            Console.WriteLine($"User {userId} added to group.");

        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"User disconnected: {Context.ConnectionId}");
        var userId = Context.User?.FindFirst("id")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnDisconnectedAsync(exception);
    }
    public async Task NotifyOwner(Guid reservationId)
    {
        Console.WriteLine($"[DEBUG] Notifikacija started for reservationId: {reservationId}");

        var reservation = await _carService.GetCarReservationsByIdAsync(reservationId);

        if (reservation == null)
        {
            throw new InvalidOperationException($"Reservation with  {reservationId} not found.");
        }

        var carListing = await _carService.GetCarListingByIdAsync(reservation.CarListingId);

        if (carListing == null)
        {
            throw new InvalidOperationException($"Car with Id :  {reservation.CarListingId} not found");
        }

        var ownerId = carListing.UserId.ToString();

        var notificationMessage = new
        {
            ReservationId = reservationId,
            Message = $"Nova rezervacija je napravljena za vaše vozilo: {carListing.Model.ManafacturerName}",
            RenterId = reservation.UserId
        };

        await Clients.Group(ownerId).SendAsync("NotifyOwner", notificationMessage);
        Console.WriteLine($"[DEBUG]Notifikacija poslana vlasniku s ID-om: {ownerId}");
    }
}