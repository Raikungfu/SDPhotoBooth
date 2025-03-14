using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCoupon(string code)
        {
            var coupon = await _couponService.GetCoupon(code);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
            var coupons = await _couponService.GetCoupons();
            return Ok(coupons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponRequestDTO couponRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExist = await _couponService.GetCoupon(couponRequestDTO.Code);
            if (isExist != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Coupon already exists");
            }
            var coupon = await _couponService.CreateCoupon(couponRequestDTO);
            return Ok(coupon);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateCoupon(string code, [FromBody] CouponRequestDTO couponRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExist = await _couponService.GetCoupon(code);
            if (isExist == null)
            {
                return NotFound();
            }
            var coupon = await _couponService.UpdateCoupon(code, couponRequestDTO);
            return Ok(coupon);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteCoupon(string code)
        {
            var isExist = await _couponService.GetCoupon(code);
            if (isExist == null)
            {
                return NotFound();
            }
            var isDeleted = await _couponService.DeleteCoupon(code);
            return Ok(isDeleted);
        }
    }
}
