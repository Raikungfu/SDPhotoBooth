using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.PaymentDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _depositService;
        private readonly FirebaseService _firebaseService;
        public DepositController(IDepositService depositService, FirebaseService firebaseService)
        {
            _depositService = depositService;
            _firebaseService = firebaseService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateDeposit([FromForm] DepositRequestDTO depositRequest)
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

                var created = await _depositService.CreateDeposit(depositRequest, userId);
                return Ok(created);
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
