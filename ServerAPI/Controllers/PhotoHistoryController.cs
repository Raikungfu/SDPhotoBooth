using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotoHistoryController : ControllerBase
    {
        private readonly IPhotoHistoryService _photoHistoryService;
        public PhotoHistoryController(IPhotoHistoryService photoHistoryService)
        {
            _photoHistoryService = photoHistoryService;
        }

        [HttpGet("{sessionCode}")]
        public async Task<IActionResult> Get(string sessionCode)
        {
            var photos = await _photoHistoryService.GetListPhotosFromSessionCode(sessionCode);
            if (photos == null)
            {
                return NotFound();
            }
            return Ok(photos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var photoHistory = await _photoHistoryService.GetPhotoHistoryAsync(id, userId);
            if (photoHistory == null)
            {
                return NotFound();
            }
            return Ok(photoHistory);
        }

        [HttpGet]
        public async Task<IActionResult> GetListPhotoHistoryAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var photoHistories = await _photoHistoryService.GetListPhotoHistoryAsync(userId);
            return Ok(photoHistories);
        }
    }

}
