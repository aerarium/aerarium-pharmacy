using AerariumTech.Pharmacy.App.Extensions;

namespace AerariumTech.Pharmacy.App.Services
{
    public class ShoppingCartItem
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        public decimal SubTotal => this.GetSubTotal();
    }
}