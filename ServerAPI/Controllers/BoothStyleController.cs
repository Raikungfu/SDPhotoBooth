using BusinessLogic.DTO.BoothStyleDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BoothStyleController : ControllerBase
    {
        private readonly IBoothStyleService _boothStyleService;
        public BoothStyleController(IBoothStyleService boothStyleService)
        {
            _boothStyleService = boothStyleService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var boothStyles = await _boothStyleService.GetAllBoothStyles();
                if (boothStyles == null)
                {
                    return NotFound();
                }
                return Ok(boothStyles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Database Query Error: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var boothStyle = await _boothStyleService.GetBoothStyle(id);
            if (boothStyle == null)
            {
                return NotFound();
            }
            return Ok(boothStyle);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoothStyleRequestDTO boothStyleRequestDTO)
        {
            if (boothStyleRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var boothStyle = await _boothStyleService.CreateBoothStyle(boothStyleRequestDTO);
            return Ok(boothStyle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BoothStyleRequestDTO boothStyleRequestDTO)
        {
            if (id <= 0 || boothStyleRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }

            var boothStyle = await _boothStyleService.GetBoothStyle(id);
            if (boothStyle == null)
            {
                return NotFound();
            }
            var updatedBoothStyle = await _boothStyleService.UpdateBoothStyle(id, boothStyleRequestDTO);

            return Ok(updatedBoothStyle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid data provided.");
            }
            var boothStyle = await _boothStyleService.GetBoothStyle(id);
            if (boothStyle == null)
            {
                return NotFound();
            }
            var deletedBoothStyle = await _boothStyleService.DeleteBoothStyle(id);
            return Ok(deletedBoothStyle);
        }
    }
}
