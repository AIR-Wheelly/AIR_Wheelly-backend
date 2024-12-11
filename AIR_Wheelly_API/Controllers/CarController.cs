using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
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
            return CreatedAtAction(nameof(GetCarListingsById), new { id = newListingId }, new{id = newListingId});

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarListingsById(Guid id)
    {
        var carListing = await _carService.GetCarListingsAsync();
        return Ok(carListing);
    }
    
    
}