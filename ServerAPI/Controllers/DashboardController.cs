using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.Service.IService;
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

        public DashboardController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }

        [HttpPost("statictis-order")]
        public async Task<IActionResult> GetStaticOrder([FromBody] StaticType staticType)
        {
            var totalOrderStatic = await orderService.StaticOrderCreated(staticType);

            return Ok(totalOrderStatic);
        }

        [HttpPost("statictis-user")]
        public async Task<IActionResult> GetStaticUser([FromBody] StaticType staticType)
        {
            var totalUserStatic = await userService.StaticUserCreated(staticType);

            return Ok(totalUserStatic);
        }

        [HttpPost("statictis-revenue")]
        public async Task<IActionResult> GetStaticRevenue([FromBody] StaticType staticType)
        {
            var totalRevenueStatic = await orderService.StaticRevenue(staticType);

            return Ok(totalRevenueStatic);
        }
    }
}
