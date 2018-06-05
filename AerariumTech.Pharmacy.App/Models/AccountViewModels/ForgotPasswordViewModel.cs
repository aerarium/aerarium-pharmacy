using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.App.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}