using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IDepositService depositService;
        private readonly ITransactionService transactionService;
        private readonly ISessionService sessionService;

        public DashboardController(IOrderService orderService, IUserService userService, IDepositService depositService, ITransactionService transactionService, ISessionService sessionService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.depositService = depositService;
            this.transactionService = transactionService;
            this.sessionService = sessionService;
        }

        [HttpGet("statictis-order")]
        public async Task<IActionResult> GetStaticOrder([FromQuery] StaticType staticType = StaticType.Day)
        {
            var totalOrderStatic = await orderService.StaticOrderCreated(staticType);

            return Ok(totalOrderStatic);
        }

        [HttpGet("statictis-deposit")]
        public async Task<IActionResult> GetStaticDeposit([FromQuery] StaticType staticType = StaticType.Day)
        {
            var totalDepositStatic = await depositService.StaticDepositCreated(staticType);

            return Ok(totalDepositStatic);
        }

        [HttpGet("statictis-user")]
        public async Task<IActionResult> GetStaticUser([FromQuery] StaticType staticType = StaticType.Day)
        {
            var totalUserStatic = await userService.StaticUserCreated(staticType);

            return Ok(totalUserStatic);
        }

        [HttpGet("statictis-revenue")]
        public async Task<IActionResult> GetStaticRevenue([FromQuery] StaticType staticType = StaticType.Day)
        {
            var totalRevenueStatic = await transactionService.StaticRevenue(staticType);

            return Ok(totalRevenueStatic);
        }

        [HttpGet("statictis-revenue-by-platform-type")]
        public async Task<IActionResult> GetStaticRevenueByPlatformType([FromQuery] GroupingType staticType = GroupingType.Day)
        {
            var totalTransactionStatic = await transactionService.StaticRevenueByPlatformType(staticType);
            return Ok(totalTransactionStatic);
        }

        [HttpGet("statictis-usage-channel")]
        public async Task<IActionResult> GetStaticUsageChannel([FromQuery] UsageChannelFilterDTO filter)
        {
            if (filter.ChannelGroupingType == ChannelGroupingType.Booth && filter.LocationId == null)
            {
                return BadRequest("LocationId is required when channelGroupingType is Booth");
            }

            var totalUsageChannelStatic = await sessionService.StaticUsageChannel(filter);
            return Ok(totalUsageChannelStatic);
        }
    }
}
