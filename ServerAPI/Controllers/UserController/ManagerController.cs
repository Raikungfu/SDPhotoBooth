using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUserService _userService;

        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _userService.GetAll(UserType.Manager);
            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagerById(string id)
        {
            var manager = await _userService.GetById(id, UserType.Manager);
            if (manager == null) return NotFound();
            return Ok(manager);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager([FromBody] UserRequestDTO managerRequestDTO)
        {
            var id = await _userService.Create(managerRequestDTO, UserType.Manager);
            return CreatedAtAction(nameof(GetManagerById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(string id, [FromBody] UserRequestDTO managerRequestDTO)
        {
            var manager = await _userService.Update(id, managerRequestDTO);
            if (manager == null) return NotFound();
            return Ok(manager);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(string id)
        {
            var result = await _userService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
