using AutoMapper;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PaymentGateDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using ServerAPI.Services;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ITypeSessionService typeSessionService;
        private readonly IPaymentMethodService paymentMethodService;
        private readonly IUserService userService;
        private readonly ICouponService couponService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;
        private readonly IPayOSService payOSService;


        public OrderController(IOrderService orderService, ITypeSessionService typeSessionService, IPaymentMethodService paymentMethodService, IUserService userService, ICouponService couponService, IConfiguration configuration, IHostEnvironment env, IPayOSService payOSService)
        {
            this.orderService = orderService;
            this.typeSessionService = typeSessionService;
            this.paymentMethodService = paymentMethodService;
            this.userService = userService;
            this.couponService = couponService;
            _configuration = configuration;
            _env = env;
            this.payOSService = payOSService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var orders = await orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var order = await orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] OrderRequestDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (order.TypeSessionId == null || order.PaymentMethodId == null)
            {
                return BadRequest("Type session and Payment method are required.");
            }

            var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
            if (typeSession == null)
            {
                return BadRequest("Invalid type session.");
            }

            var payment = await paymentMethodService.GetPaymentMethod(order.PaymentMethodId);
            if (payment == null)
            {
                return BadRequest("Invalid payment.");
            }

            var customer = await userService.UserDetail(order.Email);
            if (customer != null)
            {
                order.UserId = customer.Id;
            }

            if (!string.IsNullOrEmpty(order.CouponCode))
            {
                var coupon = await couponService.GetCoupon(order.CouponCode);
                if (coupon != null && (coupon.StartDate == null || coupon.StartDate <= DateTime.Now)
                    && (coupon.EndDate == null || coupon.EndDate >= DateTime.Now)
                    && (coupon.MaxUse == null || coupon.MaxUse > coupon.UsedAmount)
                    && (coupon.MinOrder == null || coupon.MinOrder <= typeSession.Price)
                    && coupon.IsActive && !coupon.IsDeleted)
                {
                    order.CouponId = coupon.Id;
                    var discount = Math.Max(0m, coupon.Discount.GetValueOrDefault() + (typeSession.Price * coupon.DiscountPercent.GetValueOrDefault()));
                    order.Amount = (coupon.MaxDiscount.HasValue && coupon.MaxDiscount > 0)
                        ? Math.Max(0, typeSession.Price - Math.Min(discount, (decimal)coupon.MaxDiscount.Value))
                        : Math.Max(0, typeSession.Price - discount);
                }
                else
                {
                    return BadRequest("Invalid coupon code.");
                }
            }
            else
            {
                order.Amount = typeSession.Price;
            }

            var newOrder = await orderService.CreateOrder(order);

            if (payment.MethodName == "Cash")
            {
                return Ok(newOrder);
            }
            string clientUrl = "";
            if (_env.IsProduction())
            {
                clientUrl = _configuration["ClientUrl:Production"];
            }
            else if (_env.IsDevelopment())
            {
                clientUrl = _configuration["ClientUrl:Development"];
            }

            var paymentLink = await payOSService.CreatePaymentLink(order.Amount, payment.MethodName, newOrder, typeSession, customer, clientUrl);
            return Ok(new { newOrder, paymentLink });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] OrderStatusRequestDTO orderStatus)
        {
            var order = await orderService.GetOrder(orderStatus.OrderId);
            if (order == null)
            {
                return BadRequest();
            }

            await orderService.UpdateOrderStatus(orderStatus);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var order = await orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            await orderService.DeleteOrder(id);
            return Ok();
        }
    }



    public class WebhookRequest
    {
        public string WebhookUrl { get; set; } = string.Empty;
    }
}
