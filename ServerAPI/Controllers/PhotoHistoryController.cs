using BusinessLogic.DTO.PhotoHistoryDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var histories = await _photoHistoryService.GetAll();
            if (histories == null)
            {
                return NotFound();
            }
            return Ok(histories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var history = await _photoHistoryService.GetById(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PhotoHistoryRequestDTO photoHistoryRequestDTO)
        {
            if (photoHistoryRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var history = await _photoHistoryService.CreateHistory(photoHistoryRequestDTO);
            return Ok(history);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var history = await _photoHistoryService.GetById(id);
            if (history == null)
            {
                return NotFound();
            }
            await _photoHistoryService.DeleteHistory(id);
            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUserPhotoHistories()
        {
            var histories = await _photoHistoryService.GetByUser();
            return Ok(histories);
        }
    }
}
