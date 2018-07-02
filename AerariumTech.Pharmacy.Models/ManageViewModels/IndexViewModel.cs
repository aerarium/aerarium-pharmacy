using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}