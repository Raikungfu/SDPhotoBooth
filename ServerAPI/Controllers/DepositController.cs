using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.DepositProductDTO;
using BusinessLogic.DTO.PaymentDTO;
using BusinessLogic.DTO.TypeSessionProductDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BusinessLogic.Utils;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
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
        private readonly ILevelMembershipService _levelMembershipService;
        private readonly ICouponService _couponService;
        private readonly IDepositProductService _depositProductService;
        private readonly IPaymentService paymentService;

        public DepositController(IDepositService depositService, IPaymentService paymentService,
            ILevelMembershipService levelMembershipService, ICouponService couponService, IDepositProductService depositProductService)
        {
            _depositService = depositService;
            this.paymentService = paymentService;
            _levelMembershipService = levelMembershipService;
            _couponService = couponService;
            _depositProductService = depositProductService;
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

                #region add deposit product
                var levelMemberships = await _levelMembershipService.GetAllLevelMemberships(new PaginationParams());
                var coupons = await _couponService.GetCoupons(new PaginationParams());

                var validCoupons = new List<CouponResponseDTO>();
                foreach (var c in coupons)
                {
                    try
                    {
                        _couponService.ValidateCoupon(c, deposit.Amount);
                        validCoupons.Add(c);
                    }
                    catch { }
                }


                foreach (var level in levelMemberships)
                {
                    var depositProduct = new DepositProductCreateRequestDTO
                    {
                        Name = $"{deposit.Amount}_{level.Name}",
                        ProductId = $"{deposit.Amount}_{ProductIdHelper.Normalize(level.Name)}",
                        DepositId = deposit.Id,
                        LevelMembershipId = level.Id
                    };

                    await _depositProductService.CreateAsync(depositProduct);
                }

                foreach (var coupon in validCoupons)
                {
                    var depositProduct = new DepositProductCreateRequestDTO
                    {
                        Name = $"{deposit.Amount}_{coupon.Name}",
                        ProductId = $"{deposit.Amount}_{ProductIdHelper.Normalize(coupon.Name)}",
                        DepositId = deposit.Id,
                        CouponId = coupon.Id
                    };
                    await _depositProductService.CreateAsync(depositProduct);
                }
                #endregion

                return Ok(deposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetDeposits([FromQuery] PaginationParams pagination)
        {
            try
            {
                var result = await _depositService.GetAllDeposits(pagination);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _depositService.GetCount();
            return Ok(count);
        }

        [HttpGet("mobile")]
        public async Task<IActionResult> GetDepositsByCusId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found");
            }
            try
            {
                var result = await _depositService.GetDepositsByCusId(userId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
