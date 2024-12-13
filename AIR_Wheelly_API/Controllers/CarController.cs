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
    public CarController(ICarService carService)
    {
        _carService = carService;
    }
    [HttpGet]
    public IActionResult GetFuelType()
    {
        return Ok(_carService.GetFuelTypes());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllManafacturer()
    {
        var manafacturers = await _carService.GetAllManafacturersAsync();
        if (manafacturers == null) throw new ArgumentNullException(nameof(manafacturers));
        return Ok(manafacturers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelsById(Guid id)
    {
        var models = await _carService.GetModelsByManafacturerIdAsync(id);
        if (models == null) throw new ArgumentNullException(nameof(models));
        return Ok(models);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarListing([FromBody] CarListingDTO carListing)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid data provided");
        try
        {
            var newListingId = await _carService.CreateCarListingAsync(carListing);
            return CreatedAtAction(nameof(GetCarListingById), new { id = newListingId }, new{id = newListingId});

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarListingById(Guid id)
    {
        var carListing = await _carService.GetCarListingByIdAsync(id);

        if (carListing is null)
            return BadRequest(new { Message = $"No car listing with id {id}" });

        return Ok(carListing);
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
    
    
}