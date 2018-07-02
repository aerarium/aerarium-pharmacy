using System;
using System.Collections.Generic;
using System.Linq;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.ShoppingCartViewModels
{
    public class ShoppingCartConverter
    {
        public static IEnumerable<SaleInvoice> Convert(ShoppingCartViewModel model)
            => model.Select(x => new SaleInvoice
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            });

        public static Sale Convert(CheckoutViewModel model)
            => new Sale
            {
                ShippingRateId = model.ShippingRateId,
                Payment = new Payment
                {
                    DateOfExpiration = DateTime.UtcNow.AddMonths(6),
                    PaymentModeId = model.PaymentModeId
                }
            };
    }
}