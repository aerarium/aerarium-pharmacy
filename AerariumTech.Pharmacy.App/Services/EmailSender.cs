using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AerariumTech.Pharmacy.App.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly MessageSenderOptions _options;

        public EmailSender(IOptions<MessageSenderOptions> options, ILogger<EmailSender> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        private async Task Execute(string subject, string message, string email)
        {
            try
            {
                var to = !string.IsNullOrEmpty(email)
                    ? email
                    : throw new ArgumentException(nameof(email) + " cannot be empty");

                var mail = new MailMessage
                {
                    From = new MailAddress(_options.Email, "Farm Nat"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                mail.To.Add(new MailAddress(to));

                using (var client = new SmtpClient(_options.Domain, _options.Port)
                {
                    Timeout = TimeSpan.FromMinutes(5).Milliseconds,
                    Credentials = new NetworkCredential(_options.Email, _options.Password),
                    EnableSsl = true
                })
                {
                    await client.SendMailAsync(mail);
                }
            }
            catch
            {
                _logger.LogError($"Couldn't send email to {email}, about the subject '{subject}'.");
            }
        }

        // Deprecated (SendGrid)
        // private static Task Execute(string apiKey, string subject, string message, string email)
        // {
        //     var client = new SendGridClient(apiKey);
        // 
        //     var emailMessage = new SendGridMessage
        //     {
        //         From = new EmailAddress("contact@aerariumtech.com", "Aerarium Pharmacy"),
        //         Subject = subject,
        //         HtmlContent = message
        //     };
        //     emailMessage.AddTo(new EmailAddress(email));
        // 
        //     return client.SendEmailAsync(emailMessage);
        // }
    }
}