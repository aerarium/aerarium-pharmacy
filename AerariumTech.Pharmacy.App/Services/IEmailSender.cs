using System.Threading.Tasks;

namespace AerariumTech.Pharmacy.App.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
