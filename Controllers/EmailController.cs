using System.IO;
using DeployGame.Models;
using DeployGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeployGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmailController(ApplicationDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpPost("save")]
        public IActionResult SaveEmail([FromBody] EmailRequest request)
        {
            // Check if the user already exists by email
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            Console.WriteLine($"user = {user}; user == null = {user == null}");

            if (user == null)
            {
                user = new User { Email = request.Email };
                _context.Users.Add(user);
            }

            //get all keys for the user and check if the user has reached the maximum number of keys
            var keys = _context.Keys.Where(k => k.UserId == user.Id).ToList();

            //write in console the number of keys
            Console.WriteLine($"key.Count = {keys.Count}; user.MaxTry = {user.MaxTry}");

            if (keys.Count > user.MaxTry)
            {
                return BadRequest(new { message = "You have reached the maximum number of keys" });
            }

            var guid = Guid.NewGuid().ToString();

            var keyModel = new Key { KeyValue = guid, User = user, UserId = user.Id };
            _context.Keys.Add(keyModel);

            _context.SaveChanges();

            Console.WriteLine($"Email saved successfully for {request.Email}");
            //create a command patter to send the email
            /*
            var emailService = Environment.GetEnvironmentVariable("EMAIL_SERVICE")
                               ?? throw new ArgumentNullException("EMAIL_SERVICE");
            var emailServicePort = int.Parse(Environment.GetEnvironmentVariable("EMAIL_SERVICE_PORT") ?? "0");
            var emailUser = Environment.GetEnvironmentVariable("EMAIL_USER")
                            ?? throw new ArgumentNullException("EMAIL_USER");
            var emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                                ?? throw new ArgumentNullException("EMAIL_PASSWORD");
            var emailFrom = Environment.GetEnvironmentVariable("EMAIL_FROM")
                            ?? throw new ArgumentNullException("EMAIL_FROM");

            var mailer = new Mailer(
                emailService,
                emailServicePort,
                true,
                emailUser,
                emailPassword,
                emailFrom
            );

            var pathLocal = Path.Combine(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("FILE_HTML_EMAIL")
                ?? throw new ArgumentNullException("FILE_HTML_EMAIL")
            );
            var htmlContent = System.IO.File.ReadAllText(pathLocal);
            htmlContent = htmlContent.Replace(
                "{{description}}",
                Environment.GetEnvironmentVariable("EMAIL_DESCRIPTION") ?? string.Empty
            );
            htmlContent = htmlContent.Replace(
                "{{link}}",
                (Environment.GetEnvironmentVariable("EMAIL_ENDPOINT") ?? string.Empty)
                + "?key="
                + guid
            );

            mailer.SendMail(
                user.Email,
                Environment.GetEnvironmentVariable("EMAIL_SUBJECT") ?? string.Empty,
                htmlContent
            );
            */

            return Ok(new { message = "Email saved successfully" });
        }
    }

    public class EmailRequest
    {
        public required string Email { get; set; } = string.Empty;
    }
}