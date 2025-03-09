using BusinessLogic.DTO.SendEmailDTO;

namespace BusinessLogic.Service.IService
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string toEmail, string resetLink);
        Task<bool> SendEmailAsync(SendEmailRequestDto request);

    }
}
