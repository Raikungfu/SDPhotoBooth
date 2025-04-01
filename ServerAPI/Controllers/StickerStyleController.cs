using BusinessLogic.DTO.StickerStyleDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StickerStyleController : ControllerBase
    {
        private readonly IStickerStyleService _stickerStyleService;
        public StickerStyleController(IStickerStyleService stickerStyleService)
        {
            _stickerStyleService = stickerStyleService;
        }

        [HttpPost]
        public async Task<ActionResult<StickerStyleResponseDTO>> CreateStickerStyle(StickerStyleRequestDTO stickerStyleRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(string.IsNullOrEmpty(stickerStyleRequestDTO.StickerStyleName))
            {
                return BadRequest("Sticker Style Name is required");
            }
            var stickerStyle = await _stickerStyleService.CreateStickerStyle(stickerStyleRequestDTO);
            return Ok(stickerStyle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StickerStyleResponseDTO>> DeleteStickerStyle(int id)
        {
            var stickerStyle = await _stickerStyleService.GetStickerStyleById(id);
            if (stickerStyle == null)
            {
                return NotFound();
            }
            var deletedStickerStyle = await _stickerStyleService.DeleteStickerStyle(id);
            return Ok(deletedStickerStyle);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<StickerStyleResponseDTO>>> GetAllStickerStyles()
        {
            var stickerStyles = await _stickerStyleService.GetAllStickerStyles();
            return Ok(stickerStyles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StickerStyleResponseDTO>> GetStickerStyle(int id)
        {
            var stickerStyle = await _stickerStyleService.GetStickerStyleById(id);
            if (stickerStyle == null)
            {
                return NotFound();
            }
            return Ok(stickerStyle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StickerStyleResponseDTO>> UpdateStickerStyle(int id, StickerStyleRequestDTO stickerStyleRequestDTO)
        {
            var stickerStyle = await _stickerStyleService.GetStickerStyleById(id);
            if (stickerStyle == null)
            {
                return NotFound();
            }
            var updatedStickerStyle = await _stickerStyleService.UpdateStickerStyle(id, stickerStyleRequestDTO);
            return Ok(updatedStickerStyle);
        }
    }
}
