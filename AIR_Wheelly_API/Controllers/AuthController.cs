using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dto) {
            try {
                var result = await _authService.RegisterUser(dto);
                return CreatedAtAction(nameof(Register), result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            try
            {
                var result = await _authService.LoginUser(dto);
                return CreatedAtAction(nameof(Login), result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
