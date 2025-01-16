using AIR_Wheelly_BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticService _service;
        public StatisticsController(StatisticService service)
        {
            _service = service;
        }
    }
}
