using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces;

public interface ILocationService
{
    Task<Location> CreateLocationsAsync(Location location);
    Task<List<Location>> GetLocationsAsync();
    Task<List<Location>> GetLocationsByIdAsync(Guid Id);
    Task<Location> UpdateLocationsAsync(Guid id, Location updatedLocation);
    Task DeleteLocationsAsync(Guid id);

}