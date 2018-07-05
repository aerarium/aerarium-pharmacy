using System;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Models.ShoppingCartViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly PharmacyContext _context;

        public ProductController(PharmacyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var product =
                await _context.Products.FirstOrDefaultAsync(
                    p => p.Name.Contains(id));

            if (product == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewData["Product"] = product;
            var model = new AddToCartViewModel
            {
                ProductId = product.Id,
                Quantity = 1
            };

            return View(model);
        }

        public async Task<IActionResult> Search(string keywords)
        {
            // No amount filter included, ie, it shows even products without stock
            var products = await _context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Supplier)
                .Where(p => p.Name.Contains(keywords) ||
                            p.ProductCategories.Any(pc => pc.Category.Name.Contains(keywords)) ||
                            p.Description.Contains(keywords) || p.Supplier.Name.Contains(keywords))
                .OrderBy(p => p.Name).ToListAsync();

            return View(products);
        }
    }
}