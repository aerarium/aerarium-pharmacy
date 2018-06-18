using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AerariumTech.Pharmacy.App.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<MessageSenderOptions> options)
        {
            Options = options.Value;
        }

        public MessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        } 
        private static Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);

            var emailMessage = new SendGridMessage
            {
                From = new EmailAddress("contact@aerariumtech.com", "Aerarium Pharmacy"),
                Subject = subject,
                HtmlContent = message
            };
            emailMessage.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(emailMessage);
        }
    }
}