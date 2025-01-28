using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Exceptions;
using AIR_Wheelly_Common.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIR_Wheelly_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("clientId")]
        public async Task<IActionResult> GetClientId()
        {
            var token = await _paymentService.GenerateClientToken();
            return Ok(new {token});
        }

        [HttpPost("createPurchase")]
        public async Task<IActionResult> CreatePurchase(CreatePaymentDTO dto)
        {
            try
            {
                await _paymentService.CreateTransaction(dto);
                return Ok(new {message = "Success"});
            }
            catch (PaymentException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message, detail: ex.StackTrace, statusCode: 500);
            }

        }

    }
}
