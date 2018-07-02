using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using static AerariumTech.Pharmacy.Domain.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = Clerk + "," + Manager + "," + Pharmacist)]
    [DashboardRoute]
    public class SuppliersController : Controller
    {
        private readonly PharmacyContext _context;

        public SuppliersController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Suppliers
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            return View(suppliers);
        }

        // GET: Dashboard/Suppliers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .SingleOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Dashboard/Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Cnpj,Address,PostCode,Phone,Email")]
            Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.SingleOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Dashboard/Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Cnpj,Address,PostCode,Phone,Email")]
            Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SupplierExists(supplier.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        // GET: Dashboard/Suppliers/Delete/5
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.SingleOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Dashboard/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var supplier = await _context.Suppliers.SingleOrDefaultAsync(s => s.Id == id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}