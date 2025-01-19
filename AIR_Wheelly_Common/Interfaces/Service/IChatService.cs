using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.DTO.Response;
using Microsoft.AspNetCore.SignalR;

namespace AIR_Wheelly_Common.Interfaces.Service;

public interface IChatService
{
    Task<bool> IsUserPartOfReservationAsync(Guid reservationId, Guid userId);
    ChatDTO CreateMessage(Guid reservationId, Guid senderId, string message);
    Task AddUserToReservationAsync(IGroupManager groupManager, string connectionId, Guid userId);
    Task<(Guid OwnerId, Guid RenterId)> GetChatParticipantsAsync(Guid reservationId);

}