using BusinessLogic.DTO.WalletDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();

            return Ok(payments);
        }
        [HttpGet("mobile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetPayment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var payments = await _paymentService.GetPaymentByCusId(userId);
            
            return Ok(payments);
        }

    }
}
