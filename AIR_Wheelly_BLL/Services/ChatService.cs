using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace AIR_Wheelly_BLL.Services;

public class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task<bool> IsUserPartOfReservationAsync(Guid reservationId, Guid userId)
    {
        var reservation = await _unitOfWork.CarReservationRepository.GetByIdAsync(reservationId);
        if (reservation == null)
        {
            return false;
        }
        return reservation.UserId == userId || reservation.CarListingId == userId;
    }

    public ChatDTO CreateMessage(Guid reservationId, Guid senderId, string message)
    {
        return new ChatDTO()
        {
            ReservationId = reservationId,
            SenderId = senderId,
            Message = message
        };
    }

    public async Task AddUserToReservationAsync(IGroupManager groupManager, string connectionId, Guid userId)
    {
        var reservations = await _unitOfWork.CarReservationRepository.GetByUserIdAsync(userId);
        foreach (var reservation in reservations)
        {
            await groupManager.AddToGroupAsync(connectionId, reservation.Id.ToString());
        }
    }
}