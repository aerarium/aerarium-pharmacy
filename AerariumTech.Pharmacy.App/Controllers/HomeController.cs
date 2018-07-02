using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.Models.SharedViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly PharmacyContext _context;

        public HomeController(PharmacyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page = null)
        {
            var validBatches = _context.Products.Include(p => p.Batches).ThenInclude(b => b.Stocks)
                .Where(p => p.Batches.Any(b => b.DateOfExpiration > DateTime.UtcNow))
                .AsNoTracking(); // brings all batches of a product with a least one valid batch

            var products = validBatches.Where(p =>
                p.Batches.SelectMany(b => b.Stocks)
                    .Where(s => s.Batch.DateOfExpiration > DateTime.UtcNow && s.MovementType == MovementType.In)
                    .Sum(s => s.Quantity)
                - p.Batches.SelectMany(b => b.Stocks)
                    .Where(s => s.Batch.DateOfExpiration > DateTime.UtcNow && s.MovementType == MovementType.Out)
                    .Sum(s => s.Quantity)
                > 0);

            var model = await PaginatedList<Product>.CreateAsync(products, page ?? 1, 20);

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult FrequentlyAskedQuestions()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}