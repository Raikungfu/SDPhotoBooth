using BusinessLogic.DTO.ImageProcessingDTO;
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
            await _rabbitService.SendMessageAsync(request.ImageUrl, Guid.NewGuid().ToString(), request.option, request.personalInfo);
            return Ok("Message sent!");
        }

    }
}
