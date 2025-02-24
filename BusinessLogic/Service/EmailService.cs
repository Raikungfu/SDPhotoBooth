using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using BusinessLogic.Service.IService;

namespace BusinessLogic.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendResetPasswordEmail(string toEmail, string resetLink)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                ),
                EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:FromEmail"], "Support Team"),
                Subject = "Đặt lại mật khẩu",
                Body = $"<p>Bạn đã yêu cầu đặt lại mật khẩu. Hãy nhấp vào liên kết bên dưới:</p><p><a href='{resetLink}'>Đặt lại mật khẩu</a></p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
