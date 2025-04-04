using AutoMapper;
using BusinessLogic.DTO.LevelMembershipDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelMembershipController : ControllerBase
    {
        private readonly ILevelMembershipService _levelMembershipService;
        private readonly IMapper _mapper;
        public LevelMembershipController(ILevelMembershipService levelMembershipService, IMapper mapper)
        {
            _levelMembershipService = levelMembershipService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLevelMemberships()
        {
            var levelMemberships = await _levelMembershipService.GetAllLevelMemberships();
            return Ok(levelMemberships);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLevelMembership(int id)
        {
            var levelMembership = await _levelMembershipService.GetLevelMembershipById(id);
            if (levelMembership == null)
            {
                return NotFound();
            }
            return Ok(levelMembership);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLevelMembership([FromBody] CreateLevelMembershipRequestDTO levelMembershipRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdLevelMembership = await _levelMembershipService.CreateLevelMembership(levelMembershipRequestDTO);
            return CreatedAtAction(nameof(GetLevelMembership), new { id = createdLevelMembership.Id }, createdLevelMembership);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLevelMembership(int id, [FromBody] UpdateLevelMembershipRequestDTO levelMembershipRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingLevelMembership = await _levelMembershipService.GetLevelMembershipById(id);
            if (existingLevelMembership == null)
            {
                return NotFound();
            }
            var updatedLevelMembership = await _levelMembershipService.UpdateLevelMembership(id, levelMembershipRequestDTO);
            return Ok(updatedLevelMembership);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLevelMembership(int id)
        {
            var result = await _levelMembershipService.DeleteLevelMembership(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
