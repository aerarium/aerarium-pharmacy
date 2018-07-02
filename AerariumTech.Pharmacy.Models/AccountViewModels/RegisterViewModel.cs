using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.Validations;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "O {0} deve conter no minímo {2} e no máximo {1} caracteres.",
            MinimumLength = 8)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [CPF]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter no minímo {2} e no máximo {1} caracteres.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare(nameof(Password), ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
}