using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using System.Security.Claims;

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
        public async Task<IActionResult> Get()
        {
            var orders = await orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrderCreated()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await orderService.GetOrdersForCurrentUser(userId);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("orders-customer/{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByCusId([FromRoute] string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Order ID is required.");
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            if (!Enum.TryParse(role, out UserType userType))
            {
                return Unauthorized();
            }

            if(userType == UserType.Customer && id != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            switch (userType)
            {
                case UserType.Manager:
                case UserType.Staff:
                    var order = await orderService.GetOrdersByCusId(id);
                    return order != null ? Ok(order) : NotFound("Order not found.");

                case UserType.Customer:
                    var customerOrder = await orderService.GetOrdersByCusId(id);
                    return customerOrder != null ? Ok(customerOrder) : NotFound("No orders found for this customer.");

                default:
                    return Unauthorized();
            }
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (!Enum.TryParse(role, out UserType userType))
            {
                return Unauthorized();
            }

            var order = new OrderResponseDTO();

            switch (userType)
            {
                case UserType.Manager:
                case UserType.Staff:
                    order = await orderService.GetOrder(id, null);
                    break;
                case UserType.Customer:
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    order = await orderService.GetOrder(id, userId);
                    return Ok(order);
                default:
                    return Unauthorized();
            }

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
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

            var paymentLink = await payOSService.CreatePaymentLink(newOrder.Amount, payment.MethodName, newOrder, typeSession, customer, clientUrl);
            return Ok(new { newOrder, paymentLink });
        }

        [HttpPost("customer")]
        [Authorize]
        public async Task<IActionResult> CustomerOrder([FromBody] OrderRequestDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }
            order.UserId = userId;

            var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
            if (typeSession == null)
            {
                return BadRequest("Invalid type session.");
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

            return Ok(newOrder);
        }


        [HttpPatch]
        [Authorize(Roles = "Manager,Staff")]
        public async Task<IActionResult> Patch([FromBody] OrderStatusRequestDTO orderStatus)
        {
            var order = await orderService.GetOrder(orderStatus.OrderId, null);
            if (order == null)
            {
                return BadRequest();
            }

            await orderService.UpdateOrderStatus(orderStatus);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Staff")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await orderService.GetOrder(id, null);
            if (order == null)
            {
                return NotFound();
            }
            await orderService.DeleteOrder(id);
            return Ok();
        }
    }
}
