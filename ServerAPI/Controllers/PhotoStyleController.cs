using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoStyleController : ControllerBase
    {
        private readonly IPhotoStyleService _photoStyleService;
        public PhotoStyleController(IPhotoStyleService photoStyleService)
        {
            _photoStyleService = photoStyleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoStyleResponseDTO>>> GetAllPhotoStyles()
        {
            var photoStyles = await _photoStyleService.GetAllPhotoStyles();
            return Ok(photoStyles);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> GetPhotoStyle(int id)
        {
            var photoStyle = await _photoStyleService.GetPhotoStyleById(id);
            if (photoStyle == null)
            {
                return NotFound();
            }
            return Ok(photoStyle);
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> GetPhotoStyleByName(string name)
        {
            var photoStyle = await _photoStyleService.GetPhotoStyleByName(name);
            if (photoStyle == null)
            {
                return NotFound();
            }
            return Ok(photoStyle);
        }

        [HttpPost]
        public async Task<ActionResult<PhotoStyleResponseDTO>> CreatePhotoStyle(PhotoStyleRequestDTO photoStyleRequestDTO)
        {
            var photoStyle = await _photoStyleService.CreatePhotoStyle(photoStyleRequestDTO);
            if (photoStyle == null)
            {
                return BadRequest("PhotoStyle already exists");
            }
            return Ok(photoStyle);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> DeletePhotoStyle(int id)
        {
            var photoStyle = await _photoStyleService.DeletePhotoStyle(id);
            if (photoStyle == null)
            {
                return NotFound();
            }
            return Ok(photoStyle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> UpdatePhotoStyle(int id, PhotoStyleRequestDTO photoStyleRequestDTO)
        {
            var isExist = await _photoStyleService.GetPhotoStyleById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            var photoStyle = await _photoStyleService.UpdatePhotoStyle(id, photoStyleRequestDTO);
            if (photoStyle == null)
            {
                return BadRequest("Failed to update photoStyle");
            }
            return Ok(photoStyle);
        }
    }
}
