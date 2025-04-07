using AutoMapper;
using BusinessLogic.DTO.FrameDTO;
using BusinessLogic.Service.IService;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrameController : ControllerBase
    {
        private readonly IFrameService _frameService;
        private readonly IFrameStyleService _frameStyleService;
        private readonly IMapper _mapper;
        private readonly FirebaseService _firebaseService;

        public FrameController(IFrameService frameService, IMapper mapper, IFrameStyleService frameStyleService, FirebaseService firebaseService)
        {
            _frameService = frameService;
            _mapper = mapper;
            _frameStyleService = frameStyleService;
            _firebaseService = firebaseService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateFrame([FromForm] FrameWithFormFileDTO form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (form.FrameFile == null || form.FrameFile.Length == 0 ||
             form.FrameStyleId <= 0 ||
                form.SlotCount <= 0)
            {
                return BadRequest("Invalid input data");
            }
            var frameStyle = await _frameStyleService.GetFrameStyle(form.FrameStyleId.Value);
            if (frameStyle == null)
            {
                return BadRequest("Invalid Frame Style");
            }

            var uploadedUrl = await _firebaseService.UploadImageAsync(
            form.FrameFile.OpenReadStream(),
            form.FrameFile.FileName,
            form.FrameFile.ContentType);

            var frameRequestDTO = _mapper.Map<FrameRequestDTO>(form);
            frameRequestDTO.FrameUrl = uploadedUrl;

            var frame = await _frameService.CreateFrame(frameRequestDTO);
            return Ok(frame);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFrame(int id)
        {
            var isDeleted = await _frameService.DeleteFrame(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFrame(int id)
        {
            var frame = await _frameService.GetFrame(id);
            if (frame == null)
            {
                return NotFound();
            }
            return Ok(frame);
        }

        [HttpGet]
        public async Task<IActionResult> GetFrames([FromQuery] PaginationParams pagination)
        {
            try
            {
                var frames = await _frameService.GetFrames(pagination);
                return Ok(frames);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("frame-style/{frameStyleId:int}")]
        public async Task<IActionResult> GetFrameByFrameStyleId(int frameStyleId)
        {
            var frames = await _frameService.GetFrameByFrameStyleId(frameStyleId);
            return Ok(frames);
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateFrame(int id, [FromForm] FrameWithFormFileDTO form)
        {
            var frameRequestDTO = _mapper.Map<FrameRequestDTO>(form);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExists = await _frameService.GetFrame(id);
            if (isExists == null)
            {
                return NotFound();
            }
            if(form.FrameStyleId.HasValue)
            {
                var frameStyle = await _frameStyleService.GetFrameStyle(form.FrameStyleId.Value);
                if (frameStyle == null)
                {
                    return BadRequest("Invalid Frame Style");
                }
            }
            if(form.SlotCount.HasValue && form.SlotCount.Value <= 0) return BadRequest("Invalid SlotCount");

            if (form.FrameFile != null && form.FrameFile.Length != 0)
            {
                var uploadedUrl = await _firebaseService.UploadImageAsync(
                    form.FrameFile.OpenReadStream(),
                    form.FrameFile.FileName,
                    form.FrameFile.ContentType);

                frameRequestDTO.FrameUrl = uploadedUrl; 
            }

            var frame = await _frameService.UpdateFrame(id, frameRequestDTO);
            return Ok(frame);
        }

        [HttpGet("by-name/{frameName}")]
        public async Task<IActionResult> SearchFrame(string frameName)
        {
            var frames = await _frameService.SearchFrame(frameName);
            return Ok(frames);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _frameService.GetCount();
            return Ok(count);
        }
    }
}
