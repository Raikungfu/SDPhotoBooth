using AutoMapper;
using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.Service.IService;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PhotoStyleController : ControllerBase
    {
        private readonly IPhotoStyleService _photoStyleService;
        private readonly FirebaseService _firebaseService;
        private readonly IMapper _mapper;
        public PhotoStyleController(IPhotoStyleService photoStyleService, FirebaseService firebaseService, IMapper mapper)
        {
            _photoStyleService = photoStyleService;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _photoStyleService.GetCount();
            return Ok(count);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoStyleResponseDTO>>> GetAllPhotoStyles([FromQuery] PaginationParams pagination)
        {
            try
            {
                var photoStyles = await _photoStyleService.GetAllPhotoStyles(pagination);
                return Ok(photoStyles);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> CreatePhotoStyle([FromForm] PhotostyleWithFormFileRequestDTO form)
        {
            var photoStyleRequestDTO = _mapper.Map<PhotoStyleRequestDTO>(form);
            if (form.ImageUrl != null && form.ImageUrl.Length != 0)
            {
                var imageUrl = await _firebaseService.UploadImageAsync(form.ImageUrl.OpenReadStream(), form.ImageUrl.FileName, form.ImageUrl.ContentType);
                photoStyleRequestDTO.ImageUrl = imageUrl;
            }

            if (string.IsNullOrEmpty(form.Prompt) || string.IsNullOrEmpty(form.NegativePrompt))
            {
                return BadRequest("Prompt and Negative Prompt is required");
            }

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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> UpdatePhotoStyle(int id, [FromForm] PhotostyleWithFormFileRequestDTO form)
        {
            var isExist = await _photoStyleService.GetPhotoStyleById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<PhotoStyleRequestDTO>(form);
            if (form.ImageUrl != null && form.ImageUrl.Length != 0)
            {
                var imageUrl = await _firebaseService.UploadImageAsync(form.ImageUrl.OpenReadStream(), form.ImageUrl.FileName, form.ImageUrl.ContentType);
                dto.ImageUrl = imageUrl;
            }
            var photoStyle = await _photoStyleService.UpdatePhotoStyle(id, dto);
            if (photoStyle == null)
            {
                return BadRequest("Failed to update photoStyle");
            }
            return Ok(photoStyle);
        }
    }
}
