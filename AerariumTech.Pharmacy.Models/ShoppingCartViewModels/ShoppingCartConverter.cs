using System;
using System.Linq;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.ShoppingCartViewModels
{
    public class ShoppingCartConverter
    {
        public static SaleInvoice Convert(ShoppingCartItemViewModel model)
            => new SaleInvoice
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

        public static Sale Convert(CheckoutViewModel model)
            => new Sale
            {
                ShippingRateId = model.ShippingRateId,
                SaleInvoices = model.Items.Select(Convert).ToList(),
                Payment = new Payment
                {
                    DateOfExpiration = DateTime.UtcNow.AddMonths(6),
                    Value = model.Items.Sum(e => e.SubTotal),
                    PaymentModeId = model.PaymentModeId
                }
            };
    }
}