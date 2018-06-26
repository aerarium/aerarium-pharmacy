using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
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

        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]/{name}")]
        public async Task<IActionResult> Index(string name)
        {

            name = name.Replace("-", " ");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return View();
            }

            ViewData["Product"] = product;
            var model = new AddToCartViewModel
            {
                ProductId = product.Id,
                Quantity = 1
            };

            return View(model);
        }

        public async Task<IActionResult> Search(string keywords, int page = 1, int perPage = 1)
        {
            // No amount filter included, ie, it shows even products without stock
            var products = _context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Supplier)
                .Where(p => p.Name.Contains(keywords) ||
                            p.ProductCategories.Any(pc => pc.Category.Name.Contains(keywords)) ||
                            p.Description.Contains(keywords) || p.Supplier.Name.Contains(keywords))
                .OrderBy(p => p.Name).AsNoTracking();

            var model = await PaginatedCollection<Product>.CreateAsync(products, page, perPage);

            return View(model);
        }
    }
}