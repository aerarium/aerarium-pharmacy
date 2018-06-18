using System;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Data;
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
        public async Task<IActionResult> Get(string name)
        {
            name = name.Replace("-", " ");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Search(string keywords)
        {
            // No amount filter included, ie, it shows even products without stock
            var products = await _context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Supplier)
                .Where(p => p.Name.Contains(keywords) ||
                            p.ProductCategories.Any(pc => pc.Category.Name.Contains(keywords)) ||
                            p.Description.Contains(keywords) || p.Supplier.Name.Contains(keywords)).AsNoTracking()
                .ToListAsync();

            return View(products);
        }
    }
}