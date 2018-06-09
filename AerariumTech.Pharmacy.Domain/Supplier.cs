using AerariumTech.Pharmacy.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Domain
{
    public class Supplier
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Supplier name can contain only {0} characters.")]
        public string Name { get; set; }

        [CNPJ(ErrorMessage = "Invalid CNPJ.")]
        [StringLength(18, ErrorMessage = "CNPJs must contain {0} characters.", MinimumLength = 18)]
        public string Cnpj { get; set; }

        [StringLength(100, ErrorMessage = "Address can contain only {0} characters.")]
        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(9, ErrorMessage = "Post code can only contain {0} characters")]
        public string PostCode { get; set; }

        [Phone]
        [StringLength(20, ErrorMessage = "Phone can only contain {0} digits.")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(50, ErrorMessage = "Email can contain only {0} characters.")]
        public string Email { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}