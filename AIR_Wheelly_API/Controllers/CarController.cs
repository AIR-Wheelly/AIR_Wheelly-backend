using AIR_Wheelly_BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CarController(FuelTypeService fuelTypeService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetFuelType()
    {
        return Ok(fuelTypeService.GetFuelTypes());
    }
}