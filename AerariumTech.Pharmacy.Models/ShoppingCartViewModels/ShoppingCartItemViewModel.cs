using AerariumTech.Pharmacy.Validations;

namespace AerariumTech.Pharmacy.Models.ShoppingCartViewModels
{
    public class ShoppingCartItemViewModel
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        [Price]
        public decimal Price { get; set; }

        [Price]
        public decimal SubTotal => Quantity * Price;
    }
}