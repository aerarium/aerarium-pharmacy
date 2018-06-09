using System.Collections.Generic;
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
        [CPF]
        public string Cpf { get; set; }
        
        public ICollection<Sale> Purchases { get; set; }
        public ICollection<Sale> Sales { get; set; } // in case it's an employee
    }
}