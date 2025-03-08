using AutoMapper;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using ServerAPI.Services;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGateWebhookController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IPayOSService payOSService;
        private readonly IMapper mapper;

        public PaymentGateWebhookController(IOrderService orderService, IPayOSService payOSService, IMapper mapper)
        {
            this.orderService = orderService;
            this.payOSService = payOSService;
            this.mapper = mapper;
        }


        [HttpPost("/payos-confirm-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> PayOSConfirmWebhook([FromBody] WebhookType request)
        {
            try
            {
                if (request == null || request.data == null)
                {
                    return BadRequest(new { code = "20", desc = "Invalid request data" });
                }

                var orderCode = request.data.orderCode;
                var amount = request.data.amount;
                var success = request.success;

                if (!payOSService.VerifyPaymentWebhookData(request))
                {
                    return BadRequest(new { code = "20", desc = "Invalid Signature" });
                }

                var order = await orderService.GetOrder(orderCode);
                if (order != null && order.Amount == amount)
                {
                    order.Status = success ? OrderStatus.Completed : OrderStatus.Failed;
                    orderService.UpdateOrder(orderCode, mapper.Map<OrderRequestDTO>(order));
                }

                return Ok(new
                {
                    code = "00",
                    desc = "success",
                    data = order
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { code = "20", desc = "Internal Server Error", data = ex.Message });
            }
        }
    }
}
