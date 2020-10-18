using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Security
{
    public interface IAppEmailSender : IEmailSender
    {
        Task SendContactEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IAppEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly AuthMessageSenderOptions _options; //set only via Secret Manager

        public EmailSender(
            IConfiguration configuration, 
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _configuration = configuration;
            _options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var senderName = _configuration["Email:Name"];
            var senderEmail = _configuration["Email:Address"];
            var from = new EmailAddress(senderEmail, senderName);
            var siteName = _configuration["SiteName"];
            return Execute(_options.SendGridKey, from, subject, message, email);
        }

        public Task SendContactEmailAsync(string email, string subject, string message)
        {
            var from = new EmailAddress(email);
            var to = _configuration["ContactUsEmail"];
            return Execute(_options.SendGridKey, from, subject, message, to);
        }

        public Task Execute(string apiKey, EmailAddress from, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);

            var siteName = _configuration["SiteName"];
            subject = $"[{siteName}] {subject}";
            var msg = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}