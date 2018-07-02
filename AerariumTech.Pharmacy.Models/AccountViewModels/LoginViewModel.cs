using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário")]
        /*[EmailAddress]*/
        public string User { get; set; }

        /* [Required] */
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Continuar conectado?")]
        public bool RememberMe { get; set; }
    }
}