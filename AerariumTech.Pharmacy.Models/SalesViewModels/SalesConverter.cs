using System.Linq;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.SalesViewModels
{
    public static class SalesConverter
    {
        public static Sale Convert(CreateSaleViewModel model)
            => new Sale
            {
                SaleInvoices = model.ItemSales.Select(item => new SaleInvoice
                {
                    ProductId = item.ProductId,
                    Quantity = item.Amount
                }).ToList(),
                Customer = new User{Cpf=model.Cpf}
            };
    }
}