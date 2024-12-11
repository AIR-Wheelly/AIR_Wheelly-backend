using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Enums;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_BLL.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _context;

    public CarService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Manafacturer>> GetAllManafacturersAsync()
    {
        return await _context.Manafacturers.ToListAsync();
    }

    public async Task<IEnumerable<ModelDTO>> GetModelsByManafacturerIdAsync(Guid id)
    {
        return await _context.Models.Where(m => m.ManafacturerId == id).Select(m => new ModelDTO()
        {
            Id = m.Id,
            ManafacturerId = m.ManafacturerId,
            Name = m.Name,
        }).ToListAsync();
    }
    public IEnumerable<string> GetFuelTypes()
    {
        return Enum.GetNames(typeof(FuelTypes)).ToList();
    }

    public async Task<Guid> CreateCarListingAsync(CarListingDTO carListingDto)
    {
        var newCarListing = new CarListing()
        {
            Id = Guid.NewGuid(),
            ModelId = carListingDto.ModelId,
            YearOfProduction = carListingDto.YearOfProduction,
            FuelType = carListingDto.FuelType,
            RentalPriceType = carListingDto.RentalPrice,
            NumberOfSeats = carListingDto.NumberOfSeats,
            LocationId = carListingDto.LocationId,
            NumberOfKilometers = carListingDto.NumberOfKilometers,
            RegistrationNumber = carListingDto.RegistrationNumber,
            Description = carListingDto.Description,
            UserId = carListingDto.UserId,
            IsActive = true
        };
        _context.CarListings.Add(newCarListing);
        await _context.SaveChangesAsync();
        return newCarListing.Id;
    }
    public async Task<IEnumerable<CarListing>> GetCarListingsAsync()
    {
        var carListings = await _context.CarListings.Include(c => c.Model).ThenInclude(m => m.Manafacturer)
            .Where(cl => cl.IsActive).ToListAsync();
        return carListings;
    }

    
}