using System.Collections.Generic;

namespace AerariumTech.Pharmacy.Models.ShoppingCartViewModels
{
    public class CheckoutViewModel
    {
        /// <summary>
        /// It is a nullable long because there
        /// is the option to take the order on a
        /// physical store, which is represented by null.
        /// </summary>
        public long? ShippingRateId { get; set; }

        public short PaymentModeId { get; set; }
    }
}