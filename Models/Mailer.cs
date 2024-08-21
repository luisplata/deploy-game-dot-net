using System.Net;
using System.Net.Mail;
using DeployGame.Controllers;
using DeployGame.Services;

public class Mailer
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public Mailer(string host, int port, bool enableSsl, string username, string password, string fromEmail)
    {
        _smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = enableSsl
        };
        _fromEmail = fromEmail;
    }

    public void SendMail(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage(_fromEmail, toEmail, subject, body)
        {
            IsBodyHtml = true
        };
        _smtpClient.Send(mailMessage);
    }
}