using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.CompilerServices;

namespace AIR_Wheelly_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _service;
        private readonly JwtHelper _jwtHelper;
        public StatisticsController(IStatisticService service, JwtHelper jwtHelper)
        {
            _service = service;
            _jwtHelper = jwtHelper;
        }

        [HttpGet]
        public async Task<IActionResult> NumberOfRentsPerCar([FromHeader] string authorization)
        {
            try
            {
                var id = Guid.Parse(_jwtHelper.GetUserIdFromJwt(authorization.Replace("Bearer ", "").Trim()));
                var stats = await _service.GetNumberOfRentsPerCar(id);
                return Ok(new { result = stats });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> LastMonth([FromHeader] string authorization)
        {
            try
            {
                var id = Guid.Parse(_jwtHelper.GetUserIdFromJwt(authorization.Replace("Bearer ", "").Trim()));
                var stats = await _service.GetLastMonthsStatistics(id);
                return Ok(new { result = stats });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
