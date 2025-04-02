using AutoMapper;
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
        private readonly FirebaseService _firebaseService;
        private readonly IMapper _mapper;
        public PhotoStyleController(IPhotoStyleService photoStyleService, FirebaseService firebaseService, IMapper mapper)
        {
            _photoStyleService = photoStyleService;
            _firebaseService = firebaseService;
            _mapper = mapper;
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PhotoStyleResponseDTO>> CreatePhotoStyle([FromForm] PhotostyleWithFormFileRequestDTO form)
        {
            var photoStyleRequestDTO = _mapper.Map<PhotoStyleRequestDTO>(form);
            if (form.ImageUrl != null && form.ImageUrl.Length != 0)
            {
                var imageUrl = await _firebaseService.UploadImageAsync(form.ImageUrl.OpenReadStream(), form.ImageUrl.FileName, form.ImageUrl.ContentType);
                photoStyleRequestDTO.ImageUrl = imageUrl;
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
