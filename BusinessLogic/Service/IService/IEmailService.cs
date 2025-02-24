namespace BusinessLogic.Service.IService
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string toEmail, string resetLink);
    }
}
