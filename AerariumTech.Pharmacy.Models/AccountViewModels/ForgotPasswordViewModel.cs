using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}