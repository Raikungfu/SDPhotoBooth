using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PaymentDTO;
using BusinessLogic.DTO.TransactionDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using StackExchange.Redis;
using System.Security.Claims;
using static Azure.Core.HttpHeader;

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
        private readonly IPaymentService paymentService;
        private readonly IWalletService walletService;
        private readonly ITransactionService transactionService;

        public OrderController(IOrderService orderService, ITypeSessionService typeSessionService, IPaymentMethodService paymentMethodService, IUserService userService, ICouponService couponService, IConfiguration configuration, IHostEnvironment env, IPayOSService payOSService, IPaymentService paymentService, IWalletService walletService, ITransactionService transactionService)
        {
            this.orderService = orderService;
            this.typeSessionService = typeSessionService;
            this.paymentMethodService = paymentMethodService;
            this.userService = userService;
            this.couponService = couponService;
            _configuration = configuration;
            _env = env;
            this.payOSService = payOSService;
            this.paymentService = paymentService;
            this.walletService = walletService;
            this.transactionService = transactionService;
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

        [HttpGet("mobile")]
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
            try
            {
                ModelState.Remove("ConnectionId");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (order.TypeSessionId <= 0 || order.PaymentMethodId <= 0)
                    return BadRequest("Type session or payment method is invalid.");

                var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
                if (typeSession == null)
                    return BadRequest("Invalid type session.");

                var paymentMethod = await paymentMethodService.GetPaymentMethod(order.PaymentMethodId);
                if (paymentMethod == null)
                    return BadRequest("Invalid payment.");

                var customer = await userService.UserDetail(order.Email);
                if (customer != null)
                    order.CustomerId = customer.Id;

                var (error, updatedOrder, validCoupon) = await ApplyCouponIfAny(order, typeSession);
                if (error != null)
                    return error;

                if(paymentMethod.MethodName == "Cash")
                {
                    order.Status = OrderStatus.Completed;
                }

                var newOrder = await orderService.CreateOrder(order);

                if (newOrder == null)
                    return BadRequest("Failed to create order.");

                if (paymentMethod.MethodName == "Cash")
                {
                    var payment = await paymentService.CreatePaymentForOrder(new PaymentRequestDTO
                    {
                        OrderId = newOrder.Id,
                        Amount = newOrder.Amount,
                        PaymentMethodId = order.PaymentMethodId,
                        Status = PaymentStatus.Success
                    });

                    await transactionService.CreateTransaction(new TransactionRequestDTO
                    {
                        PaymentId = payment.Id,
                        Amount = payment.Amount,
                        Type = TransactionType.Purchase,
                        Description = $"Order {newOrder.Code} via Cash",
                    });

                    return Ok(newOrder);
                }

                string clientUrl = "";
                var domain = Request.Headers["Origin"].ToString();
                if (domain == _configuration["ClientUrl:Production"] || domain == _configuration["ClientUrl:Development"])
                {
                    clientUrl = domain;
                }
                else
                {
                    clientUrl = _env.IsDevelopment() ? _configuration["ServerUrl:Development"] : _configuration["ServerUrl:Production"];
                }

                if (order.Amount == null)
                    return BadRequest("Amount is null.");
                var paymentLink = await payOSService.CreatePaymentLink(order.Amount.Value, paymentMethod.MethodName, newOrder, typeSession, customer, clientUrl, order.ConnectionId);
                await paymentService.CreatePaymentForOrder(new PaymentRequestDTO
                {
                    OrderId = newOrder.Id,
                    Amount = newOrder.Amount,
                    PaymentMethodId = order.PaymentMethodId,
                    PaymentLink = paymentLink,
                });
                return Ok(new { newOrder, paymentLink });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mobile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerOrder([FromBody] OrderRequestDTO order)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User not authenticated.");

                order.CustomerId = userId;

                var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
                if (typeSession == null)
                    throw new Exception("Invalid type session.");

                var (error, updatedOrder, validCoupon) = await ApplyCouponIfAny(order, typeSession);
                if (error != null)
                    throw new Exception(error.ToString());

                var newOrder = await orderService.CreateOrder(order);

                var paymentMethod = await paymentMethodService.GetPaymentMethod(order.PaymentMethodId);
                if (paymentMethod == null)
                    throw new Exception("Invalid payment.");
                else if (paymentMethod.MethodName == "Wallet")
                {
                    var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (customerId == null)
                        throw new Exception("Customer not found.");

                    if(order.Amount == null)
                        throw new Exception("Amount is null.");

                    var walletUpdate = await walletService.UpdateBalanceWallet(customerId, order.Amount.Value, false);

                    if (walletUpdate != null)
                    {
                        var payment = await paymentService.CreatePaymentForOrder(new PaymentRequestDTO
                        {
                            OrderId = newOrder.Id,
                            Amount = newOrder.Amount,
                            PaymentMethodId = order.PaymentMethodId,
                            Status = PaymentStatus.Success
                        });

                        await orderService.UpdateOrderStatus(new OrderStatusRequestDTO
                        {
                            OrderId = newOrder.Id,
                            Status = OrderStatus.Completed
                        });
                    }
                }
                else
                {
                    await paymentService.CreatePaymentForOrder(new PaymentRequestDTO
                    {
                        OrderId = newOrder.Id,
                        Amount = newOrder.Amount,
                        PaymentMethodId = order.PaymentMethodId
                    });
                }
                    
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] OrderRequestDTO order)
        {
            var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
            if (typeSession == null)
                return BadRequest("Invalid session.");

            var (error, updatedOrder, validCoupon) = await ApplyCouponIfAny(order, typeSession);
            if (error != null)
                return error;

            return Ok(new
            {
                Order = updatedOrder,
                Coupon = validCoupon
            });
        }

        private async Task<(IActionResult? error, OrderRequestDTO order, CouponResponseDTO? coupon)> ApplyCouponIfAny(OrderRequestDTO order, TypeSessionResponseDTO typeSession)
        {
            if (string.IsNullOrEmpty(order.CouponCode))
            {
                order.Amount = typeSession.Price;
                return (null, order, null);
            }

            var coupon = await couponService.GetCoupon(order.CouponCode);
            if (coupon == null || !couponService.IsValidCoupon(coupon, typeSession.Price))
            {
                return (BadRequest("Invalid coupon code."), order, null);
            }

            order.CouponId = coupon.Id;
            var discount = Math.Max(0m,
                coupon.Discount.GetValueOrDefault() +
                (typeSession.Price * coupon.DiscountPercent.GetValueOrDefault()));

            order.Amount = (coupon.MaxDiscount.HasValue && coupon.MaxDiscount > 0)
                ? Math.Max(0, typeSession.Price - Math.Min(discount, (decimal)coupon.MaxDiscount.Value))
                : Math.Max(0, typeSession.Price - discount);

            return (null, order, coupon);
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
