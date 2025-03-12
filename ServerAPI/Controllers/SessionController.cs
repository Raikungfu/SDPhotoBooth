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

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var session = await _sessionService.GetSessionByCode(code);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SessionRequestDTO sessionRequestDTO)
        {
            if (!sessionRequestDTO.TypeSessionId.HasValue || !sessionRequestDTO.BoothId.HasValue || !sessionRequestDTO.OrderId.HasValue)
            {
                return BadRequest("Invalid data provided.");
            }
            var session = await _sessionService.CreateSession(sessionRequestDTO);
            if (session == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create session.");
            }
            return Ok(session);
        }

        [HttpPatch("{code}/update-status")]
        public async Task<IActionResult> UpdateStatus(string code, bool status)
        {
            var isExist = await _sessionService.GetSessionByCode(code);
            if (isExist == null)
            {
                return NotFound();
            }
            var result = await _sessionService.UpdateStatusSession(code, status);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update status.");
            }
            return Ok();
        }
        [HttpGet("CheckCode/{code}")]
        public async Task<IActionResult> ValidateSession(string code)
        {
            var isValid = await _sessionService.ValidateSession(code);
            if (isValid)
            {
                return Ok(new { message = "Session is valid" });
            }
            return BadRequest(new { message = "Session is either invalid" });
        }

    }
}