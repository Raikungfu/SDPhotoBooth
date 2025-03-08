using AutoMapper;
using BusinessLogic.DTO.OrderDTO;
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

        public OrderController(IOrderService orderService, ITypeSessionService typeSessionService, IPaymentMethodService paymentMethodService, IUserService userService, ICouponService couponService)
        {
            this.orderService = orderService;
            this.typeSessionService = typeSessionService;
            this.paymentMethodService = paymentMethodService;
            this.userService = userService;
            this.couponService = couponService;
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

            if (order.TypeSessionId != null && order.PaymentId != null)
            {
                var typeSession = await typeSessionService.GetTypeSessionCodeByIdAsync(order.TypeSessionId);
                if (typeSession != null)
                {
                    return BadRequest("Invalid type session.");
                }

                var payment = await paymentMethodService.GetPaymentMethod(order.PaymentId);
                if (payment != null)
                {
                    return BadRequest("Invalid payment.");
                }

                if (!string.IsNullOrEmpty(order.Email))
                {
                    var customer = await userService.UserDetail(order.Email);
                    if (customer != null)
                    {
                        order.UserId = customer.Id;
                    }

                    if (order.CouponCode != null)
                    {
                        var coupon = await couponService.GetCoupon(order.CouponCode);
                        if (coupon != null && (coupon.StartDate == null || coupon.StartDate <= DateTime.Now) 
                            && (coupon.EndDate == null || coupon.EndDate >= DateTime.Now) && (coupon.MaxUse == null || coupon.MaxUse > coupon.UsedAmount) 
                            && (coupon.MinOrder == null || coupon.MinOrder <= typeSession.Price) && coupon.IsActive && !coupon.IsDeleted)
                        {
                            order.CouponId = coupon.Id;
                        }
                        else
                        {
                            return BadRequest("Invalid coupon code.");
                        }

                        order.Amount = typeSession.Price - coupon.Discount;
                    }
                }

                return Ok(orderService.CreateOrder(order));
            }
            return BadRequest("Type session and Payment method is required.");
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
