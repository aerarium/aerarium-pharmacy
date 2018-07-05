using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.Validations;
using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// The user itself.
    /// This class has some basic info inherited from <see cref="IdentityUser{TKey}"/>,
    /// but can also have others included by the developer.
    /// </summary>
    public class User : IdentityUser<long>
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [CPF]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "CEP")]
        public string PostalCode { get; set; }

        [Display(Name = "Compras")]
        public ICollection<Sale> Purchases { get; set; }

        [Display(Name = "Vendas")]
        public ICollection<Sale> Sales { get; set; } // in case it's an employee

        [Display(Name = "Telefone")]
        public override string PhoneNumber { get; set; }
    }
}