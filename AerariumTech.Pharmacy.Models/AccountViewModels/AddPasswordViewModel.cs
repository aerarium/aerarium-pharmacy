using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public class AddPasswordViewModel
    {
        public string User { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
