using AutoMapper;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILocationService _locationService;
        public UserController(IUserService userService, IMapper mapper, ILocationService locationService)
        {
            _userService = userService;
            _locationService = locationService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _userService.GetCount();
            return Ok(count);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationParams pagination)
        {
            try
            {
                var users = await _userService.GetUsers(pagination);
                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserRequestDTO userRequestDTO)
        {
            var user = await _userService.CreateUser(userRequestDTO);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost("update-ban-status")]
        public async Task<IActionResult> UpdateBanStatus([FromQuery] string email, [FromQuery] bool isBanned)
        {
            var user = isBanned
                ? await _userService.BanUser(email)
                : await _userService.UnbanUser(email);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpGet("detail")]
        public async Task<IActionResult> UserDetail([FromQuery] string email)
        {
            var user = await _userService.UserDetail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole([FromQuery] string email, [FromQuery] UserType role)
        {
            var isExistUser = await _userService.UserDetail(email);
            if (isExistUser == null)
            {
                return NotFound();
            }
            var result = await _userService.ChangeRole(email, role);
            return Ok(result);
        }

        [HttpPost("staff/move-location")]
        public async Task<IActionResult> MoveLocation([FromQuery] string email, [FromQuery] int locationId)
        {
            var isExistUser = await _userService.UserDetail(email);
            if (isExistUser == null)
            {
                return NotFound();
            }
            if(isExistUser.Role != "Staff")
            {
                return BadRequest("User is not staff");
            }
            var isExistLocation = await _locationService.GetLocation(locationId);
            if (isExistLocation == null)
            {
                return BadRequest("Location is not found");
            }
            var result = await _userService.MoveLocation(email, locationId);
            return Ok(result);
        }

    }
}
