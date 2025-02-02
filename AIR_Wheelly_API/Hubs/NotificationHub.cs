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
}