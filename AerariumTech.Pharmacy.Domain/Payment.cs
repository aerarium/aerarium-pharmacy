using System;

namespace AerariumTech.Pharmacy.Domain
{
    public class Payment
    {
        public long SaleId { get; set; }

        public DateTime DateOfExpiration { get; set; }

        public decimal Value { get; set; }

        public short PaymentModeId { get; set; }

        public Sale Sale { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}
