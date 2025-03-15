using BusinessLogic.DTO.ImageProcessingDTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ImageProcessingController(RabbitMqService rabbitService, FirebaseService firebaseService)
        {
            _rabbitService = rabbitService;
            _firebaseService = firebaseService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] IFormFile image, [FromForm] string photoStyle, [FromForm] string connectionId)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            string firebaseUrl = await _firebaseService.UploadImageAsync(imageBytes);
            Console.WriteLine($"Image uploaded to Firebase: {firebaseUrl}");

            await _rabbitService.SendMessageAsync(firebaseUrl, connectionId, photoStyle);

            return Ok(new { Message = "Image sent for processing", ImageUrl = firebaseUrl });
        }

    }
}
