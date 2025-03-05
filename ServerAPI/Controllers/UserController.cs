using AutoMapper;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Get([FromBody] UserFilterDTO filter)
        {
            var users = await _userService.Filter(filter);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
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

        //uprole
        //move location for staff
    }
}
