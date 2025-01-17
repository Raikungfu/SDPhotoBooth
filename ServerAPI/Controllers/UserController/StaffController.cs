using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers.UserController
{

    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _userService;

        public StaffController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _userService.GetAll(UserType.Staff);
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(string id)
        {
            var staff = await _userService.GetById(id, UserType.Staff);
            if (staff == null) return NotFound();
            return Ok(staff);
        }
        [HttpGet("location/{locationId}")]
        public async Task<IActionResult> GetStaffByLocation(int locationId)
        {
            var staffList = await _userService.GetByLocation(locationId);
            return Ok(staffList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] UserRequestDTO staffRequestDTO)
        {
            var id = await _userService.Create(staffRequestDTO, UserType.Staff);
            return CreatedAtAction(nameof(GetStaffById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(string id, [FromBody] UserRequestDTO staffRequestDTO)
        {
            var staff = await _userService.Update(id, staffRequestDTO);
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var result = await _userService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
