using AerariumTech.Pharmacy.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Domain
{
    public class Supplier
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nome do fornecedor pode conter apenas {0} caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [CNPJ(ErrorMessage = "CNPJ inválido.")]
        [StringLength(18, ErrorMessage = "CNPJs deve conter {0} caracteres.", MinimumLength = 18)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [StringLength(100, ErrorMessage = "Endereço pode conter apenas {0} caracteres.")]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(9, ErrorMessage = "CEP pode conter apenas {0} caracteres.")]
        [Display(Name = "CEP")]
        public string PostCode { get; set; }

        [Phone]
        [StringLength(20, ErrorMessage = "Phone pode conter apenas {0} digitos.")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(50, ErrorMessage = "Email pode conter apenas {0} caracteres.")]
        public string Email { get; set; }
        
        [Display(Name = "Produtos")]
        public ICollection<Product> Products { get; set; }
    }
}