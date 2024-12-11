using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_BLL.Services;

public class LocationService : ILocationService
{
    private readonly ApplicationDbContext _context;

    public LocationService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Location> CreateLocationsAsync(Location location)
    {
        location.LocationId = Guid.NewGuid();
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }
    public async Task<List<Location>> GetLocationsAsync()
    {
        var locations = await _context.Locations.ToListAsync();
        return locations;
    }
    public async Task<List<Location>> GetLocationsByIdAsync(Guid Id)
    {
        var locations = await _context.Locations.Where(l => l.LocationId == Id).ToListAsync();
        return locations;
    }

    public async Task<Location> UpdateLocationsAsync(Guid id, Location updatedLocation)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
        {
            throw new KeyNotFoundException("Location not found");
        }
        location.Adress = updatedLocation.Adress;
        await _context.SaveChangesAsync();
        return location;    
    }

    public async Task DeleteLocationsAsync(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
        {
            throw new KeyNotFoundException("Location not found");
        }
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }
    
}
