using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethod();
            if (paymentMethods == null)
            {
                return NotFound();
            }
            return Ok(paymentMethods);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethod(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return Ok(paymentMethod);
        }

        [HttpGet("{paymentMethodName}")]
        public async Task<IActionResult> Get(string paymentMethodName)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethod(paymentMethodName);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return Ok(paymentMethod);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentMethodRequestDTO paymentMethodRequestDTO)
        {
            if (paymentMethodRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var paymentMethod = await _paymentMethodService.CreatePaymentMethod(paymentMethodRequestDTO);
            return Ok(paymentMethod);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PaymentMethodRequestDTO paymentMethodRequestDTO)
        {
            if (paymentMethodRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var paymentMethod = await _paymentMethodService.UpdatePaymentMethod(id, paymentMethodRequestDTO);
            return Ok(paymentMethod);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid data provided.");
            }
            var paymentMethod = await _paymentMethodService.GetPaymentMethod(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            var deletedPaymentMethod = await _paymentMethodService.DeletePaymentMethod(id);
            return Ok(deletedPaymentMethod);
        }

        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid data provided.");
            }
            var paymentMethod = await _paymentMethodService.GetPaymentMethod(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            var disabledPaymentMethod = await _paymentMethodService.DisablePaymentMethod(id);
            return Ok(disabledPaymentMethod);
        }
    }
}
