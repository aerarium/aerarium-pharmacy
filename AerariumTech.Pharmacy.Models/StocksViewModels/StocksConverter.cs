using System.Linq;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.StocksViewModels
{
    public class StocksConverter
    {
        public static Stock Convert(CreateStockViewModel model)
            => new Stock
            {
                Batch = new Batch
                {
                    DateOfFabrication = model.DateOfFabrication,
                    DateOfExpiration = model.DateOfExpiration,
                    ProductId = model.ProductId
                },
                MovementType = MovementType.In,
                Quantity = model.Quantity
            };

        public static DetailsStockViewModel Convert(Batch model)
            => new DetailsStockViewModel
            {
                Id = model.Id,
                Entrance = model.Stocks.Where(s => s.MovementType == MovementType.In).Sum(s => s.Quantity),
                DateOfExpiration = model.DateOfExpiration,
                DateOfFabrication = model.DateOfFabrication,
                Amount = model.Stocks.Where(s => s.MovementType == MovementType.In).Sum(s => s.Quantity) -
                         model.Stocks.Where(s => s.MovementType == MovementType.Out).Sum(s => s.Quantity),
                Product = model.Product
            };
    }
}