﻿using AIR_Wheelly_Common.Models;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Interfaces.Repository;

public interface ICarReservationRepository : IRepository<CarReservation>
{
    Task<CarReservation?> GetByIdAsync(Guid rentalId);
    Task<List<CarReservation>> GetByUserIdAsync(Guid userId);
    Task<bool> ExistsActiveRentalForCarAsync(Guid carListingId, DateTime startDate, DateTime endDate);
    Task<List<CarReservation>> GetReservationForOwner(Guid ownerId);
    Task<CarReservation?> GetByListingAndUserId(Guid listingId, Guid userId);
}