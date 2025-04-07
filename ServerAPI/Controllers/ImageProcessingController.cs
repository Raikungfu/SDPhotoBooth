using BusinessLogic.DTO.ImageProcessingDTO;
using BusinessLogic.Service.IService;
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
        private readonly IPhotoStyleService _photoStyleService;

        public ImageProcessingController(RabbitMqService rabbitService, FirebaseService firebaseService, IPhotoStyleService photoStyleService)
        {
            _rabbitService = rabbitService;
            _firebaseService = firebaseService;
            _photoStyleService = photoStyleService;
        }

        [HttpPost]
        public async Task<IActionResult> SendImageProcessing([FromForm] IFormFile image, [FromForm] int photoStyleId, [FromForm] string connectionId, [FromForm] string sessionCode)
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

            var photoStyle = await _photoStyleService.GetPhotoStyleById(photoStyleId);

            await _rabbitService.SendMessageAsync(firebaseUrl, connectionId, photoStyle, sessionCode);

            return Ok(new { Message = "Image sent for processing", ImageUrl = firebaseUrl });
        }
    }
}
