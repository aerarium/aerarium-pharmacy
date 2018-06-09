using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Data.PharmacyContext _context;

        public CategoryController(Data.PharmacyContext context)
        {
            _context = context;
        }

        public async Task<object> Index(string id)
        {
            if (id == null)
            {
                return new {error = "not found"};
            }

            var products = _context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Batches).ThenInclude(b => b.Stocks)
                .Where(p =>
                    p.ProductCategories.Any(pc =>
                        pc.Category.Name == id) // select categories requested
                    && p.Batches.Sum(b => // then select amount which has entered stock
                        b.Stocks.Where(s => s.Type == Type.In).Sum(s => s.Quantity)) -
                    p.Batches.Sum(
                        b => // and subtract by the amount that went out, so we can select only the ones we have
                            b.Stocks.Where(s => s.Type == Type.Out).Sum(s => s.Quantity)) > 0);

            if (!await products.AnyAsync())
            {
                return new {error = "no stock found"};
            }

            return products;
        }
    }
}