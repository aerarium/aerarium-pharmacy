using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using static AerariumTech.Pharmacy.Domain.Role;
using AerariumTech.Pharmacy.Models.StocksViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = Clerk + "," + Manager + "," + Pharmacist)]
    [DashboardRoute]
    public class StocksController : Controller
    {
        private readonly PharmacyContext _context;

        public StocksController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Stocks/Index
        public async Task<IActionResult> Index()
        {
            var batches = await _context.Batches
                .Include(b => b.Stocks)
                .Include(b => b.Product).ToListAsync();
            var model = batches.Select(StocksConverter.Convert).ToList();

            return View(model);
        }

        // GET: Dashboard/Stocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Batch)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Dashboard/Stocks/Create
        public IActionResult Create()
        {
            ViewData["Products"] = new SelectList(_context.Products, nameof(Product.Id), nameof(Product.Name));

            return View();
        }

        // POST: Dashboard/Stocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStockViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = StocksConverter.Convert(model);
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Products"] =
                new SelectList(_context.Products, nameof(Product.Id), nameof(Product.Name), model.ProductId);

            return View(model);
        }

        //// GET: Dashboard/Stocks/Edit/5
        //// TODO is it worth make editing?
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var stock = await _context.Stocks.SingleOrDefaultAsync(m => m.Id == id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id", stock.BatchId);
        //    return View(stock);
        //}

        //// POST: Dashboard/Stocks/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,BatchId,Quantity,MovementType")] Stock stock)
        //{
        //    if (id != stock.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(stock);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!_context.StockExists(stock.Id))
        //            {
        //                return NotFound();
        //            }

        //            throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id", stock.BatchId);
        //    return View(stock);
        //}

        // GET: Dashboard/Stocks/Delete/5
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Batch)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Dashboard/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var stock = await _context.Stocks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}