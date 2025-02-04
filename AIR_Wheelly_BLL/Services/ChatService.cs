﻿using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.SignalR;

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
        var carListing = await _unitOfWork.CarListingRepository.GetByIdAsync(reservation.CarListingId);
        if (carListing == null)
        {
            return false;
        }
        var isPartOfReservation = reservation.UserId == userId || carListing.UserId == userId;
        return isPartOfReservation;
       
    }

    public ChatDTO CreateMessage(Guid reservationId, Guid senderId, string message)
    {
        return new ChatDTO()
        {
            ReservationId = reservationId,
            SenderId = senderId,
            Message = message,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
    }

    public async Task AddUserToReservationAsync(IGroupManager groupManager, string connectionId, Guid userId)
    {
        
        var reservationsAsRenter = await _unitOfWork.CarReservationRepository.GetByUserIdAsync(userId);
        foreach (var reservation in reservationsAsRenter)
        {
            await groupManager.AddToGroupAsync(connectionId, reservation.Id.ToString());
        }
        var reservationsAsOwner = await _unitOfWork.CarReservationRepository.GetReservationForOwner(userId);
        foreach (var reservation in reservationsAsOwner)
        {
            await groupManager.AddToGroupAsync(connectionId, reservation.Id.ToString());
        }
    }
    public async Task<(Guid OwnerId, Guid RenterId)> GetChatParticipantsAsync(Guid reservationId)
    {
        var reservation = await _unitOfWork.CarReservationRepository.GetByIdAsync(reservationId);
        if (reservation == null)
        {
            throw new ArgumentException("Reservation not found");
        }

        var carListing = await _unitOfWork.CarListingRepository.GetByIdAsync(reservation.CarListingId);
        if (carListing == null)
        {
            throw new ArgumentException("Car listing not found");
        }
        
        return (OwnerId: carListing.UserId, RenterId: reservation.UserId);
    }

}