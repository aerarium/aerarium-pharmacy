using System;
using System.Collections.Generic;

namespace AerariumTech.Pharmacy.App.Models
{
    /// <summary>
    /// Main entity for making <see cref="Sale"/>s.
    /// </summary>
    public class Sale
    {
        public long Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public long UserId { get; set; }
        
        public User User { get; set; }
        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}