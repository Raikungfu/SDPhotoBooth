using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.Service.IService;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSessionController : ControllerBase
    {
        private readonly ITypeSessionService _typeSessionService;
        public TypeSessionController(ITypeSessionService typeSessionService)
        {
            _typeSessionService = typeSessionService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _typeSessionService.GetCount();
            return Ok(count);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParams pagination)
        {
            try
            {
                var typeSessions = await _typeSessionService.GetTypeSessionCodesAsync(pagination);
                return Ok(typeSessions);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var typeSession = await _typeSessionService.GetTypeSessionCodeByIdAsync(id);
            if (typeSession == null)
            {
                return NotFound();
            }
            return Ok(typeSession);
        }

        [HttpGet("by-name/{typeSessionName}")]
        public async Task<IActionResult> Get(string typeSessionName)
        {
            var typeSession = await _typeSessionService.SearchTypeSessionCodeByNameAsync(typeSessionName);
            if (typeSession == null)
            {
                return NotFound();
            }
            return Ok(typeSession);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TypeSessionRequestDTO typeSessionRequestDTO)
        {
            if (typeSessionRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var isNameExist = await _typeSessionService.GetTypeSessionCodeByNameAsync(typeSessionRequestDTO.Name);
            if (isNameExist != null)
            {
                return BadRequest("TypeSession name already exists.");
            }
            var typeSession = await _typeSessionService.CreateTypeSessionCodeAsync(typeSessionRequestDTO);
            return Ok(typeSession);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TypeSessionRequestDTO typeSessionRequestDTO)
        {
            if (typeSessionRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var isExist = await _typeSessionService.GetTypeSessionCodeByIdAsync(id);
            if (isExist != null)
            {
                var typeSession = await _typeSessionService.UpdateTypeSessionCodeAsync(id, typeSessionRequestDTO);
                return Ok(typeSession);
            }
            return BadRequest("TypeSession name already exists.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _typeSessionService.DeleteTypeSessionCodeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
