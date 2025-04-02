using AutoMapper;
using BusinessLogic.DTO.CoordinateDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {
        private readonly ICoordinateService _coordinateService;
        private readonly IFrameService _frameService;
        private readonly IMapper _mapper;

        public CoordinateController(ICoordinateService coordinateService, IFrameService frameService, IMapper mapper)
        {
            _coordinateService = coordinateService;
            _frameService = frameService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoordinate(CoordinateRequestDTO coordinate)
        {
            var isFrameExist = await _frameService.GetFrame(coordinate.FrameId);
            if (isFrameExist == null)
            {
                return NotFound("No correct Frame");
            }
            var slotCount = isFrameExist.SlotCount;
            var isCoordinateExist = await _coordinateService.GetCoordinateByFrameId(coordinate.FrameId);
            if (isCoordinateExist != null && slotCount == isCoordinateExist.Count())
            {
                return BadRequest("Frame is full");
            }

            var coordinateResponse = await _coordinateService.CreateCoordinate(coordinate);
            return Ok(coordinateResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(int id)
        {

            var coordinate = await _coordinateService.DeleteCoordinate(id);
            if (coordinate == null)
            {
                return NotFound();
            }
            return Ok(coordinate);
        }

        [HttpGet("by-frame/{frameId}")]
        public async Task<IActionResult> GetCoordinateByFrameId(int frameId)
        {
            var isFrameExist = await _frameService.GetFrame(frameId);
            if (isFrameExist == null)
            {
                return NotFound("No correct Frame");
            }
            var coordinate = await _coordinateService.GetCoordinateByFrameId(frameId);
            if (coordinate == null)
            {
                return NotFound();
            }
            return Ok(coordinate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoordinate(int id, UpdateCoordinateDTO coordinate)
        {
            var isExitCorrdinate = await _coordinateService.GetCoordinate(id);
            if (isExitCorrdinate == null)
            {
                return NotFound("No correct Coordinate");
            }
            
            var coordinateResponse = await _coordinateService.UpdateCoordinate(id, coordinate);
            if (coordinateResponse == null)
            {
                return NotFound();
            }
            return Ok(coordinateResponse);
        }
    }
}
