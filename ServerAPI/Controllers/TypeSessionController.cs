using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.TypeSessionProductDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSessionController : ControllerBase
    {
        private readonly ITypeSessionService _typeSessionService;
        private readonly ITypeSessionProductService _typeSessionProductService;
        private readonly ILevelMembershipService _levelMembershipService;
        public TypeSessionController(ITypeSessionService typeSessionService, ITypeSessionProductService typeSessionProductService,
            ILevelMembershipService levelMembershipService)
        {
            _typeSessionService = typeSessionService;
            _typeSessionProductService = typeSessionProductService;
            _levelMembershipService = levelMembershipService;
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

            if(typeSession.ForMobile)
            {
                //Create TypeSessionProduct
                var levelMemberships = await _levelMembershipService.GetAllLevelMemberships(new PaginationParams());
                var rand = new Random();

                foreach (var levelMembership in levelMemberships)
                {
                    var typeSessionProduct = new TypeSessionProductCreateRequestDTO
                    {
                        Name = $"{typeSession.Name} - {levelMembership.Name}",
                        ProductId = $"{typeSession.Name}-{levelMembership.Name}-{rand.Next(100000, 999999)}",
                        TypeSessionId = typeSession.Id,
                        LevelMembershipId = levelMembership.Id,
                    };
                    await _typeSessionProductService.CreateAsync(typeSessionProduct);
                }
            } 

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
            if (isExist == null)  return BadRequest("TypeSession name already exists.");

            var typeSession = await _typeSessionService.UpdateTypeSessionCodeAsync(id, typeSessionRequestDTO);
            return Ok(typeSession);
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
