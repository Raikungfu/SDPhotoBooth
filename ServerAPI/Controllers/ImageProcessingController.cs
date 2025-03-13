using BusinessLogic.DTO.ImageProcessingDTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProcessingController : ControllerBase
    {
        private readonly RabbitMqService _rabbitService;

        public ImageProcessingController(RabbitMqService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ImageProcessingDTO request)
        {
            await _rabbitService.SendMessageAsync(request.ImageUrl, Guid.NewGuid().ToString(), request.PhotoStyle);
            return Ok("Message sent!");
        }

    }
}
