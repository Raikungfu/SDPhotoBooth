using AutoMapper;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Net.payOS.Types;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ITypeSessionService typeSessionService;
        private readonly IPaymentMethodService paymentMethodService;
        private readonly IUserService userService;
        private readonly ICouponService couponService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;


        public OrderController(IOrderService orderService, ITypeSessionService typeSessionService, IPaymentMethodService paymentMethodService, IUserService userService, ICouponService couponService, IConfiguration configuration, IHostEnvironment env)
        {
            this.orderService = orderService;
            this.typeSessionService = typeSessionService;
            this.paymentMethodService = paymentMethodService;
            this.userService = userService;
            this.couponService = couponService;
            _configuration = configuration;
            _env = env;
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

        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> Post([FromBody] OrderRequestDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (order.TypeSessionId == null || order.PaymentId == null)
            {
                return BadRequest("Type session and Payment method are required.");
            }

            var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
            if (typeSession == null)
            {
                return BadRequest("Invalid type session.");
            }

            var payment = await paymentMethodService.GetPaymentMethod(order.PaymentId);
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
                    order.Amount = Math.Max(0, typeSession.Price - coupon.Discount);
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

            var paymentLink = await createPaymentLink(order.Amount, payment.MethodName, newOrder, typeSession, customer, clientUrl);
            return Ok(new { newOrder, paymentLink });
        }

        private async Task<string> createPaymentLink(decimal amount, string paymentMethod, OrderResponseDTO order, TypeSessionResponseDTO typeSession, UserResponseDTO user, string clientUrl)
        {
            var payOS = new PayOS(
                _configuration["PAYOS_CLIENT_ID"],
                _configuration["PAYOS_API_KEY"],
                _configuration["PAYOS_CHECKSUM_KEY"]
            );

            var paymentData = new PaymentData(
                orderCode: order.Id,
                amount: (int)Math.Round(amount),
                description: $"TECH-X - Order {order.Id}",
                items: new List<ItemData>
                {
                    new ItemData(
                        name: $"{typeSession.Name}",
                        quantity: 1,
                        price: (int)typeSession.Price
                    )
                },
                returnUrl: $"{clientUrl}/confirm-payment-payos",
                cancelUrl: $"{clientUrl}/confirm-payment-payos",
                buyerEmail: user.Email,
                buyerPhone: user.PhoneNumber
            );

            var paymentResponse = await payOS.createPaymentLink(paymentData);
            if (paymentResponse == null || string.IsNullOrEmpty(paymentResponse.checkoutUrl))
            {
                throw new Exception("Failed to create payment link");
            }
            return paymentResponse.checkoutUrl;
        }

        [HttpPost("confirm-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmWebhook([FromBody] Dictionary<string, string> requestBody)
        {
            var response = new
            {
                error = 0,
                message = "ok",
                data = ""
            };

            var payOS = new PayOS(
                _configuration["PAYOS_CLIENT_ID"],
                _configuration["PAYOS_API_KEY"],
                _configuration["PAYOS_CHECKSUM_KEY"]
            );

            try
            {
                if (requestBody.TryGetValue("webhookUrl", out var webhookUrl))
                {
                    var str = await payOS.confirmWebhook(webhookUrl);
                    response = new
                    {
                        error = 0,
                        message = "ok",
                        data = str
                    };
                }
                else
                {
                    response = new
                    {
                        error = -1,
                        message = "Missing 'webhookUrl' parameter.",
                        data = ""
                    };
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                response = new
                {
                    error = -1,
                    message = ex.Message,
                    data = ""
                };
                return StatusCode(500, response);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] OrderStatusRequestDTO orderStatus)
        {
            var order = orderService.GetOrder(orderStatus.OrderId);
            if (order == null)
            {
                return BadRequest();
            }

            await orderService.UpdateOrderStatus(orderStatus);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            await orderService.DeleteOrder(id);
            return Ok();
        }
    }
}
