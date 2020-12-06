using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Core.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
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
        private readonly IEmailRepository _emailRepository;
        private readonly AuthMessageSenderOptions _options; //set only via Secret Manager

        public EmailSender(
            IConfiguration configuration, 
            IEmailRepository emailRepository,
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _configuration = configuration;
            _emailRepository = emailRepository;
            _options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var senderName = _configuration["Email:Name"];
            var senderEmail = _configuration["Email:Address"];
            var from = new EmailAddress(senderEmail, senderName);
            var siteName = _configuration["SiteName"];
            await ExecuteAsync(_options.SendGridKey, from, subject, message, email);
        }

        public async Task SendContactEmailAsync(string email, string subject, string message)
        {
            var from = new EmailAddress(email);
            var to = _configuration["ContactUsEmail"];
            await ExecuteAsync(_options.SendGridKey, from, subject, message, to);
        }

        public async Task<Response> ExecuteAsync(string apiKey, EmailAddress from, string subject, string message, string email)
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
           
            var response = await client.SendEmailAsync(msg);

            var emailItem = new Email()
            {
                Id = Guid.NewGuid().ToString(),
                FromEmail = from.Email,
                FromName = from.Name,
                To = email,
                Message = message,
                DateSent = DateTimeOffset.UtcNow.ToString("s"),
                ResponseStatusCode = response.StatusCode.ToString(),
                ResponseHeaders = response.Headers.ToString(),
                ResponseBody = response.Body.ReadAsStringAsync().Result.ToString()
            };
            _emailRepository.Add(emailItem);
            await _emailRepository.SaveChangesAsync();

            return response;
        }
    }
}