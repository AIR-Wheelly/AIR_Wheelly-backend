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
        try
        {
            var userId = Context.User?.FindFirst("id")?.Value;
            if (userId == null || !Guid.TryParse(userId, out var senderId))
            {
                throw new UnauthorizedAccessException("Invalid user");
            }

            var isValidUser = await _chatService.IsUserPartOfReservationAsync(reservationId, senderId);
            if (!isValidUser)
            {
                throw new UnauthorizedAccessException("User is not part of reservation.");
            }

            var (ownerId, renterId) = await _chatService.GetChatParticipantsAsync(reservationId);
            if (senderId != ownerId && senderId != renterId)
            {
                throw new UnauthorizedAccessException("User is not authorized to participate in this chat");
            }

            var chatMessage = _chatService.CreateMessage(reservationId, senderId, message);
            Console.WriteLine($"Broadcasting message to group {reservationId}: {message}");

            await Clients.Group(reservationId.ToString()).SendAsync("ReceiveMessage", chatMessage);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while sending a message for ReservationId {reservationId}. Please try again later.", ex);

        }
    }


    public override async Task OnConnectedAsync()
    {
        try
        {
            var userId = Context.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
            {
                await _chatService.AddUserToReservationAsync(Groups, Context.ConnectionId, userIdGuid);
                Console.WriteLine($"User {userIdGuid} connected and added to groups.");
            }
            else
            {
                
                throw new UnauthorizedAccessException("User failed to connect due to invalid ID");

            }

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error in OnConnectedAsync: {ex.Message}");
        }
    }
}