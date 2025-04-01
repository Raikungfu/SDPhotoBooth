using BusinessLogic.DTO.StickerDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class StickerController : ControllerBase
    {
        private readonly IStickerService _stickerService;
        private readonly IStickerStyleService _stickerStyleService;
        public StickerController(IStickerService stickerService, IStickerStyleService stickerStyleService)
        {
            _stickerService = stickerService;
            _stickerStyleService = stickerStyleService;
        }

        [HttpPost]
        public async Task<ActionResult<StickerResponseDTO>> CreateSticker(StickerRequestDTO stickerRequestDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(string.IsNullOrEmpty(stickerRequestDTO.StickerUrl))
            {
                return BadRequest("Sticker Url is required");
            }

            if (stickerRequestDTO.StickerStyleId.HasValue)
            {
                var stickerStyle = await _stickerStyleService.GetStickerStyleById(stickerRequestDTO.StickerStyleId.Value);
                if (stickerStyle == null)
                {
                    return BadRequest("Sticker Style does not exist");
                }
            }
            var sticker = await _stickerService.CreateSticker(stickerRequestDTO);
            return Ok(sticker);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StickerResponseDTO>> DeleteSticker(int id)
        {
            var sticker = await _stickerService.GetStickerById(id);
            if (sticker == null)
            {
                return NotFound();
            }
            var deletedSticker = await _stickerService.DeleteSticker(id);
            return Ok(deletedSticker);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<StickerResponseDTO>>> GetAllStickers()
        {
            var stickers = await _stickerService.GetAllStickers();
            return Ok(stickers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StickerResponseDTO>> GetSticker(int id)
        {
            var sticker = await _stickerService.GetStickerById(id);
            if (sticker == null)
            {
                return NotFound();
            }
            return Ok(sticker);
        }

        [HttpGet("by-style/{styleId}")]
        public async Task<ActionResult<IEnumerable<StickerResponseDTO>>> GetStickerByStyle(int styleId)
        {
            var stickerStyle = await _stickerStyleService.GetStickerStyleById(styleId);
            if (stickerStyle == null)
            {
                return BadRequest("Sticker Style does not exist");
            }
            var stickers = await _stickerService.GetStickerByStyleId(styleId);
            return Ok(stickers);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StickerResponseDTO>> UpdateSticker(int id, StickerRequestDTO stickerRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sticker = await _stickerService.GetStickerById(id);
            if (sticker == null)
            {
                return NotFound();
            }

            if (stickerRequestDTO.StickerStyleId.HasValue)
            {
                var stickerStyle = await _stickerStyleService.GetStickerStyleById(stickerRequestDTO.StickerStyleId.Value);
                if (stickerStyle == null)
                {
                    return BadRequest("Sticker Style does not exist");
                }
            }

            var updatedSticker = await _stickerService.UpdateSticker(id, stickerRequestDTO);
            return Ok(updatedSticker);
        }
    }
}
