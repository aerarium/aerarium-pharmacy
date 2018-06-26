using System.Collections.Generic;
using AerariumTech.Pharmacy.App.Extensions;

namespace AerariumTech.Pharmacy.App.Services
{
    /// <summary>
    /// I don't know why the hell I did this class this way,
    /// but it represents the Shopping Cart, and contains
    /// the minimum of content, so it uses the least of the
    /// client's cache, and avoids trading unecessary information.
    /// </summary>
    public class ShoppingCart
    {
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal Total => this.GetTotal();
    }
}