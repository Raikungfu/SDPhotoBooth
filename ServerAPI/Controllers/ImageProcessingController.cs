using BusinessLogic.DTO.ImageProcessingDTO;
using BusinessLogic.Utils;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServerAPI.Hubs;
using ServerAPI.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProcessingController : ControllerBase
    {
        private readonly RabbitMqService _rabbitService;
        private readonly FirebaseService _firebaseService;
        private readonly IHubContext<TranferPhotoHub> _hubContext;

        public ImageProcessingController(RabbitMqService rabbitService, FirebaseService firebaseService, IHubContext<TranferPhotoHub> hubContext)
        {
            _rabbitService = rabbitService;
            _firebaseService = firebaseService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendImageProcessing([FromForm] IFormFile image, [FromForm] string photoStyle, [FromForm] string connectionId)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            string firebaseUrl;
            using (var stream = image.OpenReadStream())
            {
                firebaseUrl = await _firebaseService.UploadImageAsync(stream, image.FileName, image.ContentType);
            }

            string queue = QueueRoutingHelper.GetQueueName(photoStyle);

            await _rabbitService.SendMessageAsync(firebaseUrl, connectionId, photoStyle, queue);

            return Ok(new { Message = "Image sent for processing", ImageUrl = firebaseUrl, Queue = queue });
        }
    }
}
