using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.PaymentDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using StackExchange.Redis;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _depositService;
        private readonly IPaymentService paymentService;

        public DepositController(IDepositService depositService, IPaymentService paymentService)
        {
            _depositService = depositService;
            this.paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateDeposit([FromBody] DepositRequestDTO depositRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User not found");
                }

                var deposit = await _depositService.CreateDeposit(depositRequest, userId);

                var payment = await paymentService.CreatePaymentForDeposit(new PaymentRequestDTO
                {
                    DepositId = deposit.Id,
                    Amount = deposit.Amount,
                    PaymentMethodId = depositRequest.PaymentMethodId
                });

                deposit.PaymentId = payment.Id;
                return Ok(deposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetDeposits()
        {
            var result = await _depositService.GetAllDeposits();
            return Ok(result);
        }
    }
}
