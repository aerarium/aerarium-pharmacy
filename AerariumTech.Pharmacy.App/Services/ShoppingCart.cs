using System;
using System.Collections.Generic;
using System.Linq;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.App.Services
{
    public class ShoppingCart
    {
        public Guid Id { get; } = Guid.NewGuid();
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }

    public static class ShoppingCartExtensions
    {
        public static decimal GetTotal(this ShoppingCart cart) => cart.Items.Sum(e => e.GetSubTotal());

        public static decimal GetSubTotal(this ShoppingCartItem item) =>
            (item.Product.PriceWithoutDiscount ?? item.Product.Price) * item.Quantity;
    }

    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}