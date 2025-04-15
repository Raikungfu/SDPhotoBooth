using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SendImageProcessing([FromForm] ImageProcessingDTO request)
        {
            if (request.image == null || request.image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            string firebaseUrl;
            using (var stream = request.image.OpenReadStream())
            {
                firebaseUrl = await _firebaseService.UploadImageAsync(stream, request.image.FileName, request.image.ContentType);
            }

            var photoStyle = await _photoStyleService.GetPhotoStyleById(request.photoStyleId);

            await _rabbitService.SendMessageAsync(firebaseUrl, request.connectionId, photoStyle, request.sessionCode);

            return Ok(new { Message = "Image sent for processing", ImageUrl = firebaseUrl });
        }
    }
}
