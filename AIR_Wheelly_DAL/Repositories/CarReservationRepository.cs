using AIR_Wheelly_Common.Enums;
using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_DAL.Repositories;

public class CarReservationRepository : Repository<CarReservation>, ICarReservationRepository
{
    public CarReservationRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<CarReservation?> GetByIdAsync(Guid rentalId)
    {
        return await _context.CarReservations
            .Include(r => r.CarListing)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == rentalId);
    }
    
    public async Task<List<CarReservation>> GetByUserIdAsync(Guid userId)
    {
        return await _context.CarReservations
            .Include(r => r.CarListing)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<CarReservation>> GetByCarListingIdAsync(Guid carListingId)
    {
        return await _context.CarReservations
            .Where(r => r.CarListingId == carListingId)
            .ToListAsync();
    }
    public async Task<bool> ExistsActiveRentalForCarAsync(Guid carListingId , DateTime startDate, DateTime endDate)
    {
        return await _context.CarReservations
            .AnyAsync(r => r.CarListingId == carListingId &&
                           r.Status == RentalStatus.Confirmed &&
                           ((startDate >= r.StartDate && startDate < r.EndDate) ||
                            (endDate > r.StartDate && endDate <= r.EndDate) ||
                            (startDate <= r.StartDate && endDate >= r.EndDate)));
    }

}