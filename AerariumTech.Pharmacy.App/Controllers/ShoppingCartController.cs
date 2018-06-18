using System;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly PharmacyContext _context;

        public ShoppingCartController(PharmacyContext context)
        {
            _context = context;
        }

        public const string Cart = "Cart";

        public IActionResult Index()
        {
            return View(SetCartWhenEmpty());
        }

        [HttpPost]
        public IActionResult AddToCart(Product model, int amount)
        {
            var qtdStock = _context.Stocks.Include(s => s.Batch).Where(s =>
                    s.Batch.ProductId == model.Id &&
                    s.Batch.DateOfExpiration >=
                    DateTime.Today) // can only sell the product until its expiration day
                .Sum(s => s.Quantity);

            SetCartWhenEmpty();

            if (qtdStock > 0)
            {
                if (amount > qtdStock)
                {
                    ViewData["Status"] = $"Could only add {qtdStock} to cart.";
                    return View(nameof(Index));
                }

                ViewData["Status"] = $"{amount} added to cart.";
                return View(nameof(Index));
            }

            ViewData["Status"] = "Unable to add to cart!";
            // ReSharper disable once Mvc.ViewNotResolved
            return View(nameof(Index));
        }

        [HttpPost]
        public Task<IActionResult> RemoveFromCart(Product product)
        {
            var cart = ShoppingCart;

            cart.Items.Remove(cart.Items.FirstOrDefault(e => e.Product.Id == product.Id));

            HttpContext.Response.Cookies.Set(Cart, cart);

            return null;
        }

        public IActionResult ReduceAmountOf(Product product, int amount = 1)
        {
            var cart = ShoppingCart;

            var item = cart.Items.FirstOrDefault(e => e.Product.Id == product.Id);

            if (item != null)
            {
                item.Quantity -= amount;

                if (item.Quantity <= 0)
                {
                    cart.Items.Remove(item);
                }

                HttpContext.Response.Cookies.Set(Cart, cart);
            }

            return View(nameof(Index));
        }

        public IActionResult IncreaseAmountOf(Product product, int amount)
        {
            var cart = ShoppingCart;
            var item = cart.Items.FirstOrDefault(e => e.Product.Id == product.Id);

            if (item == null)
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity += amount;
            }

            return View(nameof(Index));
        }

        public Task<IActionResult> Complete()
        {



            return null;
        }

        private ShoppingCart SetCartWhenEmpty()
        {
            if (HttpContext.Request.Cookies.Get<ShoppingCart>(Cart) == null)
            {
                HttpContext.Response.Cookies.Set(Cart, new ShoppingCart());
            }

            return HttpContext.Request.Cookies.Get<ShoppingCart>(Cart);
        }

        private ShoppingCart ShoppingCart => HttpContext.Request.Cookies.Get<ShoppingCart>(Cart);
    }
}