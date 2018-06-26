using System;
using System.Linq;
using AerariumTech.Pharmacy.App.Services;
using Microsoft.AspNetCore.Http;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class ShoppingCartExtensions
    {
        public static decimal GetTotal(this ShoppingCart cart) => cart.Items.Sum(e => e.GetSubTotal());

        public static decimal GetSubTotal(this ShoppingCartItem item) => item.Price * item.Quantity;

        /// <summary>
        /// Get the cart from request cookies.
        /// </summary>
        /// <returns></returns>
        public static ShoppingCart GetShoppingCart(this HttpContext context)
            => context.Request.Cookies.Get<ShoppingCart>(Cart) ?? new ShoppingCart();

        /// <summary>
        /// Set the cart on the response cookies.
        /// </summary>
        public static ShoppingCart SetShoppingCart(this HttpContext context, ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new NullReferenceException(nameof(cart) + " cannot be null");
            }

            return context.Response.Cookies.Set(Cart, cart);
        }

        /// <summary>
        /// Expire the cart cookie.
        /// </summary>
        /// <param name="context"></param>
        public static void DeleteShoppingCart(this HttpContext context)
            => context.Response.Cookies.Delete(Cart);

        public const string Cart = nameof(Cart);
    }
}