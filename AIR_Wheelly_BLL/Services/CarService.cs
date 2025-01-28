using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Enums;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;


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
            RentalPriceType = carListingDto.RentalPriceType,
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

    public async Task<CarListingResponse> GetCarListingByIdAsync(Guid id)
    {
        var carListing = await _unitOfWork.CarListingRepository.GetCarListingWithDetailsAsync(id);
        return new CarListingResponse()
        {
            Id = carListing.Id,
            ModelId = carListing.ModelId,
            YearOfProduction = carListing.YearOfProduction,
            FuelType = carListing.FuelType,
            NumberOfSeats = carListing.NumberOfSeats,
            RentalPriceType = carListing.RentalPriceType,
            NumberOfKilometers = carListing.NumberOfKilometers,
            RegistrationNumber = carListing.RegistrationNumber,
            Description = carListing.Description,
            UserId = carListing.UserId,
            IsActive = carListing.IsActive,
            LocationId = carListing.LocationId,
            Location = carListing.Location,
            Model = carListing.Model,
            CarListingPictures = carListing.CarListingPictures
        };
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
    public async Task<CarReservationResponse> CreateRentalAsync(Guid userId, CarReservationDTO dto)
    {
        if (dto.StartDate >= dto.EndDate)
        {
            throw new ArgumentException("End date must be after start date.");

        }
        var carListing = await _unitOfWork.CarListingRepository.GetCarListingById(dto.CarListingId);
        if (carListing == null)
        {
            throw new InvalidOperationException("No  car listing found or car is inactive.");
        }
        var alReadyRented = await _unitOfWork.CarReservationRepository.ExistsActiveRentalForCarAsync(carListing.Id, dto.StartDate, dto.EndDate);
        if (alReadyRented)
        {
            throw new InvalidOperationException($"Car is already rented");
        }
       
        var rental = new CarReservation()
        {
            CarListingId = carListing.Id,
            UserId = userId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = carListing.RentalPriceType * (dto.EndDate - dto.StartDate).Days,
            Status = RentalStatus.Confirmed,
            IsPaid = false,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.CarReservationRepository.AddAsync(rental);
        await _unitOfWork.CompleteAsync();
        var carReservationResponse = new CarReservationResponse()
        {
            Id = rental.Id,
            UserId = rental.UserId,
            CarListingId = rental.CarListingId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            TotalPrice = rental.TotalPrice,
            Status = rental.Status.ToString(),
            IsPaid = rental.IsPaid,
            
        };
        return carReservationResponse;
    }

    public async Task<IEnumerable<CarReservationResponse>> GetCarReservationsAsync(Guid userId)
    {
        var reservations = await _unitOfWork.CarReservationRepository.GetByUserIdAsync(userId);
        return reservations.Select(r => new CarReservationResponse()
        {
            Id = r.Id,
            UserId = r.UserId,
            CarListingId = r.CarListingId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalPrice = r.TotalPrice,
            Status = r.Status.ToString(),
            IsPaid = r.IsPaid,
        }).ToList();
    }
    public async Task<IEnumerable<CarReservationResponse>> GetCarReservationsForOwner(Guid ownerId)
    {
        var reservations = await _unitOfWork.CarReservationRepository.GetReservationForOwner(ownerId);
        return reservations.Select(r => new CarReservationResponse()
        {
            Id = r.Id,
            UserId = r.UserId,
            CarListingId = r.CarListingId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalPrice = r.TotalPrice,
            Status = r.Status.ToString(),
            IsPaid = r.IsPaid,
        }).ToList();
    }

}