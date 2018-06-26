using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly PharmacyContext _context;

        public CategoryController(PharmacyContext context)
        {
            _context = context;
        }

        [Route("[controller]/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                id = string.Empty;
            }

            var products = await _context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Batches).ThenInclude(b => b.Stocks)
                .Where(p =>
                    p.ProductCategories.Any(pc =>
                        pc.Category.Name == id) // select categories requested
                    && p.Batches.Sum(b => // then select amount which has entered stock
                        b.Stocks.Where(s => s.MovementType == MovementType.In).Sum(s => s.Quantity)) -
                    p.Batches.Sum(
                        b => // and subtract by the amount that went out, so we can select only the ones we have
                            b.Stocks.Where(s => s.MovementType == MovementType.Out).Sum(s => s.Quantity)) > 0)
                .AsNoTracking().ToListAsync();

            return View(products);
        }
    }
}