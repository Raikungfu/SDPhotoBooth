using AutoMapper;
using BusinessLogic.DTO.MembershipCardDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipCardController : ControllerBase
    {
        private readonly IMembershipCardService _membershipCardService;
        private readonly IUserService _userService;
        private readonly ILevelMembershipService _levelMembershipService;
        private readonly IMapper _mapper;
        public MembershipCardController(IMembershipCardService membershipCardService, IMapper mapper, IUserService userService, ILevelMembershipService levelMembershipService)
        {
            _membershipCardService = membershipCardService;
            _mapper = mapper;
            _userService = userService;
            _levelMembershipService = levelMembershipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var membershipCards = await _membershipCardService.GetAll();
            return Ok(membershipCards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var membershipCard = await _membershipCardService.GetById(id);
            if (membershipCard == null)
            {
                return NotFound();
            }
            return Ok(membershipCard);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var membershipCard = await _membershipCardService.GetByCustomerId(customerId);
            if (membershipCard == null)
            {
                return NotFound();
            }
            return Ok(membershipCard);
        }

        [HttpGet("level/{levelMembershipId}")]
        public async Task<IActionResult> GetByLevelMembershipId(int levelMembershipId)
        {
            var membershipCards = await _membershipCardService.GetByLevelMembershipId(levelMembershipId);
            if (membershipCards == null || !membershipCards.Any())
            {
                return NotFound();
            }
            return Ok(membershipCards);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMembershipCardRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.UserDetailById(dto.CustomerId);
            if (user == null)
                return NotFound($"User with ID {dto.CustomerId} not found.");

            var level = await _levelMembershipService.GetLevelMembershipById(dto.LevelMemberShipId);
            if (level == null)
                return NotFound($"Level Membership with ID {dto.LevelMemberShipId} not found.");

            dto.Points = level.Point ?? 0;
            dto.Expired = DateTime.Now.AddMonths(level.Duration ?? 0);
            dto.IsActive = true;

            var membershipCard = await _membershipCardService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = membershipCard.Id }, membershipCard);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMembershipCardRequestDTO dto)
        {
            var existingCard = await _membershipCardService.GetById(id);
            if (existingCard == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.CustomerId))
            {
                var user = await _userService.UserDetailById(dto.CustomerId);
                if (user == null)
                    return NotFound($"User with ID {dto.CustomerId} not found.");
            }

            if (dto.LevelMemberShipId.HasValue)
            {
                var level = await _levelMembershipService.GetLevelMembershipById(dto.LevelMemberShipId.Value);
                if (level == null)
                    return NotFound($"Level Membership with ID {dto.LevelMemberShipId} not found.");

                dto.Points = level.Point;
                dto.Expired = DateTime.Now.AddMonths(level.Duration ?? 0);
                dto.IsActive = true;
            }

            if (dto.Points.HasValue)
            {
                if (dto.Points < 0)
                    return BadRequest("Points cannot be negative.");

                if (dto.Points > existingCard.LevelMemberShip.Point)
                    return BadRequest("Points cannot exceed LevelMemberShip.");
            }

            var updatedCard = await _membershipCardService.Update(id, dto);
            return Ok(updatedCard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _membershipCardService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
