using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Service.IService;
using BusinessLogic.DTO.SendEmailDTO;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public SendEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequestDto sendEmailRequestDto)
        {
            if (string.IsNullOrEmpty(sendEmailRequestDto.toEmail) || string.IsNullOrEmpty(sendEmailRequestDto.photoUrl))
            {
                return BadRequest("Email người nhận và ảnh không được để trống.");
            }

            var result = await _emailService.SendEmailAsync(sendEmailRequestDto);

            if (result)
                return Ok("Gửi email thành công.");
            else
                return StatusCode(500, "Gửi email thất bại.");
        }
    }
}
