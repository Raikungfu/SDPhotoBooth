using AutoMapper;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sessions = await _sessionService.GetAllSessions();
            if (sessions == null)
            {
                return NotFound();
            }
            return Ok(sessions);
        }

        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var session = await _sessionService.SearchSessionByCode(code);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var session = await _sessionService.GetSessionById(Id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost("{orderCode}")]
        public async Task<IActionResult> Post(long orderCode)
        {
            var session = await _sessionService.CreateSession(orderCode);
            if (session == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create session.");
            }
            return Ok(session); 
        }


        [HttpPost("use-session")]
        public async Task<IActionResult> UseSession([FromBody] SessionRequestDTO request)
        {
            var session = await _sessionService.GetSessionByCode(request.Code);
            if (session == null)
            {
                return NotFound();
            }

            var result = await _sessionService.UseSession(request.Code, request.BoothId);

            return Ok(result); 
        }


    }
}