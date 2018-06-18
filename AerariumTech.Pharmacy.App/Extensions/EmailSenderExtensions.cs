using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class EmailSenderExtensions
    {
        public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            var template = "Please, confirm your account by clicking this link: <a href=\"{0}\">link</a>";

            // using (var sr = new StreamReader("", Encoding.UTF8))
            // {
            //     template = await sr.ReadToEndAsync();
            // }

            await emailSender.SendEmailAsync(email, "FarmNat - Confirm your email", // TODO: ADD EMAIL TEMPLATES
                string.Format(template, HtmlEncoder.Default.Encode(link)));
        }

        public static Task SendEmailResetAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Reset Password",
                $"Please, reset your password by clicking here: <a href=\"{HtmlEncoder.Default.Encode(link)}\">link</a>");
        }
    }
}