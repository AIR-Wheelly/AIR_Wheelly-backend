using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces;

public interface ICarService
{
    Task<IEnumerable<Manafacturer>> GetAllManafacturersAsync();
    Task<IEnumerable<ModelDTO>> GetModelsByManafacturerIdAsync(Guid Id);
    public IEnumerable<string> GetFuelTypes();
    Task<Guid> CreateCarListingAsync(CarListingDTO carListingDto);
    Task<IEnumerable<CarListing>> GetCarListingsAsync();
}