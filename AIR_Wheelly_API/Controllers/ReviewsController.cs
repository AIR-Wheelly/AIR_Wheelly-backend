using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly JwtHelper _jwtHelper;  
        public ReviewsController(IReviewService reviewService, JwtHelper jwtHelper)
        {
            _reviewService = reviewService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody]CreateReviewDTO dto, [FromHeader] string authorization)
        {
            try
            {
                var userId = Guid.Parse(_jwtHelper.GetUserIdFromJwt(authorization.Replace("Bearer ", "").Trim()));
                var review = await _reviewService.CreateReview(dto, userId);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(new { ex.InnerException.Message });
                }
                return BadRequest(new { ex.Message });
            }
        }
    }
}
