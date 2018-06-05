using System.Collections.Generic;
using AerariumTech.Pharmacy.App.Validations;
using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.App.Models
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
        
        public ICollection<Sale> Sales { get; set; }
    }
}