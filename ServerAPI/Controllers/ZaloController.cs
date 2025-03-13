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
        private readonly IUserService _userService;

        public ZaloController(IPhotoHistoryService photoHistoryService, IUserService userService)
        {
            this.photoHistoryService = photoHistoryService;
            _userService = userService;
        }

        [HttpGet("send-photobooth-via-zalo/{PhoneNumber}")]
        public async Task<IActionResult> SendPhotoBoothViaZalo([FromRoute] string PhoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(PhoneNumber))
                {
                    return BadRequest(new { message = "Data invalid!" });
                }

                var user = await _userService.UserDetailByPhone(PhoneNumber);
                if (user == null || user.Role != "Customer")
                {
                    return BadRequest(new { message = "User not found!" });
                }

                var photoUrls = await photoHistoryService.GetListNewestPhotos(user.Id);

                string photoUrlsString = string.Join("\n", photoUrls);

                return Ok(new { photoUrls = photoUrlsString, customerName = user.FullName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error occured!", error = ex.Message });
            }
        }
    }
}