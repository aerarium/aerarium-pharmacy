using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.Models.CustomersViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [AdminOnly]
    [DashboardRoute]
    public class CustomersController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly UserManager<User> _userManager;

        public CustomersController(PharmacyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dashboard/Customers
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var customers = users.Where(u => !_userManager.GetRolesAsync(u).Await().Any());

            return View(customers);
        }

        // GET: Dashboard/Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Dashboard/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = CustomersConverter.Convert(model);

                await _userManager.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Dashboard/Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.Value.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var model = CustomersConverter.Convert(user);
            return View(model);
        }

        // POST: Dashboard/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditCustomerViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = CustomersConverter.Convert(model);

                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(model.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Dashboard/Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Dashboard/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(long id)
        {
            return _userManager.FindByIdAsync(id) != null;
        }
    }
}