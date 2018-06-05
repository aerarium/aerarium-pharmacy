using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email", // TODO: ADD EMAIL TEMPLATES
                string.Format("Please, confirm your account by clicking this link: <a href=\"{0}\">link</a>", HtmlEncoder.Default.Encode(link)));
        }

        public static Task SendEmailResetAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Reset Password",
                $"Please, reset your password by clicking here: <a href=\"{HtmlEncoder.Default.Encode(link)}\">link</a>");
        }
    }
}