using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using static AerariumTech.Pharmacy.Domain.Role;
using AerariumTech.Pharmacy.Models.SalesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = Clerk + "," + Manager + "," + Pharmacist)]
    [DashboardRoute]
    public class SalesController : Controller
    {
        private readonly PharmacyContext _context;

        public SalesController(PharmacyContext context)
        {
            _context = context;
        }

        #region

        // GET: Dashboard/Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sale = SalesConverter.Convert(model);
                await _context.Sales.AddAsync(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        #endregion

        #region Read and Delete methods

        // GET: Dashboard/Sales/Index
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales.ToListAsync();

            return View(sales);
        }

        // GET: Dashboard/Sales/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Dashboard/Sales/Delete/5
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Dashboard/Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sale = await _context.Sales.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}