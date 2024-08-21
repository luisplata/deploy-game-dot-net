using DeployGame.Controllers;
namespace DeployGame.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailRequest request);
    }
}