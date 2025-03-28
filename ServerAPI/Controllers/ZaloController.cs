using BusinessLogic.DTO.ZaloDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/zalo")]
    [AllowAnonymous]
    public class ZaloController : ControllerBase
    {
        private readonly IPhotoHistoryService photoHistoryService;

        public ZaloController(IPhotoHistoryService photoHistoryService, IUserService userService)
        {
            this.photoHistoryService = photoHistoryService;
        }

        [HttpGet("send-photobooth-via-zalo")]
        public async Task<IActionResult> SendPhotoBoothViaZalo([FromQuery] string phone, [FromQuery] string sessionCode, [FromQuery] string fullName)
        {
            try
            {
                if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(sessionCode))
                {
                    return BadRequest("Phone and sessionCode are required.");
                }

                var photoUrls = await photoHistoryService.GetListPhotosFromSessionCode(sessionCode);

                string photoUrlsString = string.Join("\n", photoUrls);

                return Ok(new { photoUrls = photoUrlsString });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error occured!", error = ex.Message });
            }
        }
    }
}