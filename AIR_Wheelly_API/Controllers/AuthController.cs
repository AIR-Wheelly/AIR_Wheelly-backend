using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            try
            {
                var result = await _authService.RegisterUser(dto);
                return CreatedAtAction(nameof(Register), result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            try
            {
                var token = await _authService.LoginUser(dto);
                if (token == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                return Ok(new { Token = token });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile([FromHeader] string authorization)
        {
            try
            {
                var jwtToken = authorization?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Unauthorized();
                }

                var user = await _authService.GetUserByJwt(jwtToken);
                if (user == null)
                {
                    return Unauthorized("Invalid token");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TokenLogin(OAuthLoginDTO dto)
        {
            if (dto.Token == string.Empty)
                return BadRequest();

            var token = await _authService.OAuthLogin(dto.Token);

            if (token is null)
                return BadRequest();

            return Ok(new { Token = token });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromHeader] string authorization,
            [FromBody] UpdateProfileDTO dto)
        {
            try
            {
                var jwtToken = authorization?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Unauthorized();
                }
                var updatedUser = await _authService.UpdateProfileAsync(dto,jwtToken);
                return Ok(new {message = "Updated", user = updatedUser});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
