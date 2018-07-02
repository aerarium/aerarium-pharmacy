using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class EmailSenderExtensions
    {
        public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string pathToTemplates, string link)
        {
            string template;
            
            using (var sr = new StreamReader(Path.Combine(pathToTemplates, "EmailConfirmation.cshtml")))
            {
                template = await sr.ReadToEndAsync();
            }

            await emailSender.SendEmailAsync(email, "Farm Nat - Confirme seu email",
                string.Format(template, HtmlEncoder.Default.Encode(link)));
        }

        public static async Task SendEmailResetAsync(this IEmailSender emailSender, string email, string pathToTemplates, string link)
        {
            string template;

            using (var sr = new StreamReader(Path.Combine(pathToTemplates, "EmailReset.cshtml")))
            {
                template = await sr.ReadToEndAsync();
            }

            await emailSender.SendEmailAsync(email, "Farm Nat - Resetar senha",
                string.Format(template, HtmlEncoder.Default.Encode(link)));
        }

        public static async Task SendEmailWelcomeAsync(this IEmailSender emailSender, string email,
            string pathToTemplates, string link)
        {
            string template;

            using (var sr = new StreamReader(Path.Combine(pathToTemplates, "Welcome.cshtml")))
            {
                template = await sr.ReadToEndAsync();
            }

            await emailSender.SendEmailAsync(email, "Farm Nat - Bem vindo!", string.Format(template));
        }
    }
}