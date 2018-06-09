using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        /*[EmailAddress]*/
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}