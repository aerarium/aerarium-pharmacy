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
        public string Name { get; set; }

        [CPF]
        public string Cpf { get; set; }
        
        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public ICollection<Sale> Purchases { get; set; }
        public ICollection<Sale> Sales { get; set; } // in case it's an employee
    }
}