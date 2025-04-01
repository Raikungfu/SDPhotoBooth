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

        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetCouponByCode(string code)
        {
            var coupon = await _couponService.GetCoupon(code);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        [HttpGet("search/{code}")]
        public async Task<IActionResult> SearchCoupon(string code)
        {
            var coupons = await _couponService.GetCoupons(code);
            if (coupons == null) return NotFound();
            return Ok(coupons);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponService.GetCouponById(id);
            if (coupon == null) return NotFound();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] CouponRequestDTO couponRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExist = await _couponService.GetCouponById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            var coupon = await _couponService.UpdateCoupon(id, couponRequestDTO);
            return Ok(coupon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var isExist = await _couponService.GetCouponById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            var isDeleted = await _couponService.DeleteCoupon(id);
            return Ok(isDeleted);
        }
    }
}
