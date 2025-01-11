using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Enums;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_BLL.Services;

public class CarService : ICarService
{
    private readonly IUnitOfWork _unitOfWork;

    public CarService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Manafacturer>> GetAllManafacturersAsync()
    {
        return await _unitOfWork.ManafacturerRepository.GetAllAsync();
    }

    public async Task<IEnumerable<ModelDTO>> GetModelsByManafacturerIdAsync(Guid id)
    {
        var models = await _unitOfWork.ModelRepository.GetModelsByManafacturerIdAsync(id);

        return models.Select(m => new ModelDTO()
        {
            Id = m.Id,
            ManafacturerId = m.ManafacturerId,
            Name = m.Name,
        });
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
        await _unitOfWork.CarListingRepository.AddAsync(newCarListing);
        await _unitOfWork.CompleteAsync();

        return newCarListing.Id;
    }

    public async Task<IEnumerable<CarListing>> GetCarListingsAsync()
    {
        return await _unitOfWork.CarListingRepository.GetCarListingsWithDetailsAsync();
    }

    public async Task<CarListing?> GetCarListingByIdAsync(Guid id)
    {
        return await _unitOfWork.CarListingRepository.GetCarListingWithDetailsAsync(id);
    }

    public async Task UploadCarListingPictures(IEnumerable<byte[]> files,  Guid listingId)
    {
        var listing = await _unitOfWork.CarListingRepository.GetByIdAsync(listingId);
        if (listing is null)
            throw new ArgumentNullException(nameof(CarListing));

        var listingPicutres = files.Select(f => new CarListingPicture()
        {
            CarListingId = listingId,
            Image = Convert.ToBase64String(f)
        });

        await _unitOfWork.CarListingPicturesRepository.AddRangeAsync(listingPicutres);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<CarReservation> CreateRentalAsync(Guid carListingId, Guid userId)
    {
        var carListing = await _unitOfWork.CarListingRepository.GetByIdAsync(carListingId);
        if (carListing == null)
        {
            throw new ArgumentException("Car listing not found.");
        }

        var rental = new CarReservation()
        {
            CarListingId = carListingId,
            UserId = userId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            TotalPrice = carListing.RentalPriceType,
            Status = RentalStatus.Confirmed,
            IsPaid = false,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.CarReservationRepository.AddAsync(rental);
        await _unitOfWork.CompleteAsync();
        return rental;
    }
    
}