using AutoMapper;
using BusinessLogic.DTO.MembershipCardDTO;
using BusinessLogic.Service.IService;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipCardController : ControllerBase
    {
        private readonly IMembershipCardService _membershipCardService;
        private readonly ILevelMembershipService _levelMembershipService;
        public MembershipCardController(IMembershipCardService membershipCardService, IMapper mapper, IUserService userService, ILevelMembershipService levelMembershipService)
        {
            _membershipCardService = membershipCardService;
            _levelMembershipService = levelMembershipService;
        }


        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllMembershipCards([FromQuery] PaginationParams pagination)
        {
            try
            {
                var result = await _membershipCardService.GetAll(pagination);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Staff")]
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
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var membershipCard = await _membershipCardService.GetByCustomerId(customerId);
            if (membershipCard == null)
            {
                return NotFound();
            }
            return Ok(membershipCard);
        }

        [HttpGet("mobile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetForCus()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var membershipCard = await _membershipCardService.GetByCustomerId(userId);
            if (membershipCard == null)
            {
                membershipCard = await _membershipCardService.Create(new CreateMembershipCardRequestDTO()
                {
                    CustomerId = userId,
                    LevelMemberShipId = 1,
                    Points = 0,
                    IsActive = true
                });
            }
            return Ok(membershipCard);
        }

        [HttpGet("level/{levelMembershipId}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetByLevelMembershipId(int levelMembershipId)
        {
            var membershipCards = await _membershipCardService.GetByLevelMembershipId(levelMembershipId);
            if (membershipCards == null || !membershipCards.Any())
            {
                return NotFound();
            }
            return Ok(membershipCards);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMembershipCardRequestDTO dto)
        {
            var existingCard = await _membershipCardService.GetById(id);
            if (existingCard == null)
                return NotFound();

            if (dto.LevelMemberShipId.HasValue)
            {
                var level = await _levelMembershipService.GetLevelMembershipById(dto.LevelMemberShipId.Value);
                if (level == null)
                    return NotFound($"Level Membership with ID {dto.LevelMemberShipId} not found.");
            }

            if (dto.Points.HasValue && dto.Points < 0)
            {
                return BadRequest("Points cannot be negative.");
            }

            var updatedCard = await _membershipCardService.Update(id, dto);
            return Ok(updatedCard);
        }

        [HttpPut("upgrade-level/{id}")]
        [Authorize(Roles ="Staff, Customer")]
        public async Task<IActionResult> UpgradeLevel(int id, int levelId)
        {
            var existingCard = await _membershipCardService.GetById(id);
            if (existingCard == null)
                return NotFound();

            if (!existingCard.IsActive)
                return BadRequest("Cannot upgrade. Membership card is not active.");

            var level = await _levelMembershipService.GetLevelMembershipById(levelId);
            if (level == null)
                return NotFound($"Level Membership with ID {levelId} not found.");

            if (existingCard.LevelMemberShip.Id >= levelId)
                return BadRequest($"Cannot upgrade. Target level {levelId} is not higher than current level {existingCard.LevelMemberShip.Id}.");

            if (existingCard.Points < level.Point)
                return BadRequest($"Insufficient points ({existingCard.Points}) to upgrade to level {levelId} (requires {level.Point}).");

            var dto = new UpdateMembershipCardRequestDTO
            {
                LevelMemberShipId = levelId,
                Points = existingCard.Points - level.Point
            };

            var updatedCard = await _membershipCardService.Update(id, dto);
            return Ok(updatedCard);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _membershipCardService.GetCount();
            return Ok(count);
        }
    }
}
