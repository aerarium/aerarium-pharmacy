using System;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Data;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Models;
using AerariumTech.Pharmacy.App.Services;
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
                    return View($"Could only add {qtdStock} to cart.");
                }

                return View($"{amount} added to cart.");
            }

            // ReSharper disable once Mvc.ViewNotResolved
            return View("Not able to add to cart!");
        }

        [HttpPost]
        public Task<IActionResult> RemoveFromCart(Product product)
        {
            HttpContext.Session.Get<Product>("Cart");

            return null;
        }
        
        [Route("category/{category?}")] // TODO THE FOLLOWING DOESNT BELONG HERE
        public async Task<object> GetByCategory(string category)
        {
            if (category == null)
            {
            }

            var products = _context.Products.Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(p =>
                    p.ProductCategories.Select(pc => pc.Category.Name).Any(pn => pn == category));

            if (!await products.AnyAsync())
            {
                return category + " not found";
            }

            return products;
        }

        public Task<IActionResult> ReduceAmountOf(Product product)
        {
            return null;
        }

        public Task<IActionResult> IncreaseAmountOf(Product product)
        {
            return null;
        }

        private ShoppingCart SetCartWhenEmpty()
        {
            if (HttpContext.Session.Get<ShoppingCart>(Cart) == null)
            {
                HttpContext.Session.Set(Cart, new ShoppingCart());
            }

            return HttpContext.Session.Get<ShoppingCart>(Cart);
        }
    }
}