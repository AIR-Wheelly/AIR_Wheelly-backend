using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locations;

    public LocationController(ILocationService locations)
    {
        _locations = locations;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] Location location)
    {
        var newLocation = await _locations.CreateLocationsAsync(location);
        return CreatedAtAction(nameof(GetLocationsById), new { id = newLocation.LocationId }, newLocation);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetLocationsById(Guid Id)
    {
        var locations = await _locations.GetLocationByIdAsync(Id);
        return Ok(locations);
    }
    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _locations.GetLocationsAsync();
        return Ok(locations);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateLocation(Guid Id, [FromBody] Location location)
    {
        try
        {
            var updatedLocation = await _locations.UpdateLocationsAsync(Id, location);
            return Ok(updatedLocation);

        }
        catch (KeyNotFoundException)
        {
            return NotFound(new {Message = "Location not found"});
            
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteLocation(Guid Id)
    {
        try
        {
            await _locations.DeleteLocationsAsync(Id);
            return NoContent();

        }
        catch (KeyNotFoundException)
        {
            return NotFound(new {Message = "Location not found"});
        }
    }

    
}