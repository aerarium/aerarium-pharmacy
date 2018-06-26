using System.Collections.Generic;

namespace AerariumTech.Pharmacy.Domain
{
    public class PaymentMode
    {
        public short Id { get; set; }

        public string Description { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}