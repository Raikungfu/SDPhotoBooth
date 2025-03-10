using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public DashboardController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] StaticType staticType)
        {
            var totalOrderStatic = await orderService.StaticOrderCreated(staticType);
            var totalUserStatic = await userService.StaticUserCreated(staticType);
            var totalRevenueStatic = await orderService.StaticRevenue(staticType);

            return Ok(new
            {
                totalOrderStatic,
                totalUserStatic,
                totalRevenueStatic
            });
        }
    }
}
