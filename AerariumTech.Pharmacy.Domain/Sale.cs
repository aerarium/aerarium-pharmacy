using System;
using System.Collections.Generic;

namespace AerariumTech.Pharmacy.Domain
{
    /// <summary>
    /// Main entity for making <see cref="Sale"/>s.
    /// </summary>
    public class Sale
    {
        public long Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public long CustomerId { get; set; }
        public long? SellerId { get; set; }
        public long? ShippingRateId { get; set; }
        public long PaymentId { get; set; }
        
        public User Customer { get; set; }
        public User Seller { get; set; }
        public ShippingRate ShippingRate { get; set; }
        public Payment Payment { get; set; }
        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}