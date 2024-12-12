using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_BLL.Services;

public class LocationService : ILocationService
{
    private readonly IUnitOfWork _unitOfWork;

    public LocationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Location> CreateLocationsAsync(Location location)
    {
        location.LocationId = Guid.NewGuid();
        await _unitOfWork.LocationRepository.AddAsync(location);
        await _unitOfWork.CompleteAsync();
        return location;
    }

    public async Task<List<Location>> GetLocationsAsync()
    {
        return await _unitOfWork.LocationRepository.GetAllAsync();
    }

    public async Task<Location?> GetLocationByIdAsync(Guid Id)
    {
        return await _unitOfWork.LocationRepository.GetByIdAsync(Id);
    }

    public async Task<Location> UpdateLocationsAsync(Guid id, Location updatedLocation)
    {
        var location = await GetLocationByIdAsync(id);
        if (location == null)
        {
            throw new KeyNotFoundException("Location not found");
        }
        location.Adress = updatedLocation.Adress;
        _unitOfWork.LocationRepository.Update(location);
        await _unitOfWork.CompleteAsync();
        return location;    
    }

    public async Task DeleteLocationsAsync(Guid id)
    {
        var location = await GetLocationByIdAsync(id);
        if (location == null)
        {
            throw new KeyNotFoundException("Location not found");
        }
        _unitOfWork.LocationRepository.Delete(location);
        await _unitOfWork.CompleteAsync();
    }
    
}
