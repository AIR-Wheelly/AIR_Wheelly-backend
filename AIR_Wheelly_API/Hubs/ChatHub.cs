using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AIR_Wheelly_API.Hubs;
[Authorize]
public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(Guid reservationId, string message)
    {
        var userId = Context.User?.FindFirst("id")?.Value;
        var isValidUser = userId != null && await _chatService.IsUserPartOfReservationAsync(reservationId, Guid.Parse(userId));
        if (!isValidUser)
        {
            throw new UnauthorizedAccessException("User is not authorized to send messages in this chat");
        }

        if (userId != null)
        {
            var chatMessage = _chatService.CreateMessage(reservationId, Guid.Parse(userId), message);
            await Clients.Group(reservationId.ToString()).SendAsync("ReceiveMessage", chatMessage);
        }
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst("id")?.Value;
        if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
        {
            await _chatService.AddUserToReservationAsync(Groups,Context.ConnectionId, userIdGuid);
        }
        await base.OnConnectedAsync();
    }
}