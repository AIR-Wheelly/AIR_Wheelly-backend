using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces.Service;

public interface ICarService
{
    Task<IEnumerable<Manafacturer>> GetAllManafacturersAsync();
    Task<IEnumerable<ModelDTO>> GetModelsByManafacturerIdAsync(Guid Id);
    IEnumerable<string> GetFuelTypes();
    Task<Guid> CreateCarListingAsync(CarListingDTO carListingDto);
    Task<IEnumerable<CarListing>> GetCarListingsAsync();
    Task<CarListing?> GetCarListingByIdAsync(Guid id);
    Task UploadCarListingPictures(IEnumerable<byte[]> files, Guid listingId);
    Task<CarReservationResponse> CreateRentalAsync(Guid userId,CarReservationDTO dto);
    Task<IEnumerable<CarReservationResponse>> GetCarReservationsAsync(Guid userId);
    Task<IEnumerable<CarReservationResponse>> GetCarReservationsForOwner(Guid ownerId);
}