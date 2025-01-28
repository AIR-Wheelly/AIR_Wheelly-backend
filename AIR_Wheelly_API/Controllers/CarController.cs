using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly JwtHelper _jwtHelper;
    public CarController(ICarService carService, JwtHelper jwtHelper)
    {
        _carService = carService;
        _jwtHelper = jwtHelper;
    }
    [HttpGet]
    public IActionResult GetFuelType()
    {
        var fuelTypes = _carService.GetFuelTypes();
        return Ok(new { result = fuelTypes });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllManafacturer()
    {
        var manafacturers = await _carService.GetAllManafacturersAsync();
        if (manafacturers == null)
            throw new ArgumentNullException(nameof(manafacturers));
        return Ok(new { result = manafacturers });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelsById(Guid id)
    {
        var models = await _carService.GetModelsByManafacturerIdAsync(id);
        if (models == null)
            throw new ArgumentNullException(nameof(models));
        return Ok(new { result = models });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarListing([FromBody] CarListingDTO carListing)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        try
        {
            var newListingId = await _carService.CreateCarListingAsync(carListing);
            return CreatedAtAction(nameof(GetCarListingById), new { id = newListingId }, new { id = newListingId });

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> CarListings()
    {
        var carListings = await _carService.GetCarListingsAsync();
        return Ok(new { result = carListings });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarListingById(Guid id)
    {
        var carListing = await _carService.GetCarListingByIdAsync(id);

        if (carListing is null)
            return BadRequest(new { Message = $"No car listing with id {id}" });

        return Ok(new { result = carListing });
    }

    [HttpPost]
    public async Task<IActionResult> UploadCarListingPicture([FromForm] IList<IFormFile> files, [FromForm] Guid listingId)
    {
        try
        {
            IEnumerable<byte[]> filesBytes = files.Select(f =>
            {
                using var memoryStream = new MemoryStream();
                f.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return fileBytes;
            });
            await _carService.UploadCarListingPictures(filesBytes, listingId);

            return Created();
        }
        catch (ArgumentNullException ex)
        {
            if (ex.Message == nameof(CarListing))
                return BadRequest(new { Message = "Car listing does not exist" });

            return BadRequest(new { ex.Message });
        }

    }
     [HttpPost]
     public async Task<IActionResult> CreateRental([FromBody] CarReservationDTO dto)
     {
         try
         {
             var userId = GetUserIdFromToken();
             var rental = await _carService.CreateRentalAsync(userId,dto);
             return Ok(rental);
         }
         catch (InvalidOperationException ex)
         {
             return BadRequest(new { message = ex.Message });
         }
         catch (ArgumentException ex)
         {
             return NotFound(new { message = ex.Message });
         }
     }

     [HttpGet]
     public async Task<IActionResult> GetReservationsByUser()
     {
         var userId = GetUserIdFromToken();
         var reservations = await _carService.GetCarReservationsAsync(userId);

         return Ok(new {result = reservations});
     }

     [HttpGet]
     public async Task<IActionResult> GetReservationsForMyCars()
     {
         var userId = GetUserIdFromToken();
         var reservations = await _carService.GetCarReservationsForOwner(userId);
         return Ok(new {result = reservations});   
     }


     private Guid GetUserIdFromToken()
     {
         var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer","").Trim();
         var userIdString = _jwtHelper.GetUserIdFromJwt(token);
         if (!Guid.TryParse(userIdString, out  var userId))
         {
             throw new UnauthorizedAccessException("Invalid token");
         }
         return userId;
         
     }


}