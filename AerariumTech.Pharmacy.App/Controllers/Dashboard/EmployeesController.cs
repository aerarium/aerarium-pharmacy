using System;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.Models.EmployeesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [AdminOnly]
    [DashboardRoute]
    public class EmployeesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public EmployeesController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Dashboard/Employees
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var employees = users.Where(u =>
            { // TODO do it can be improved?
                var task = _userManager.GetRolesAsync(u);
                task.Wait();
                var roles = task.Result;
                return roles.Any();
            }).ToList();

            return View(employees);
        }

        // GET: Dashboard/Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userManager.FindByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Dashboard/Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Roles"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), nameof(Role.Name),
                nameof(Role.Name));

            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = EmployeesConverter.Convert(model);
                await _userManager.CreateAsync(employee);
                await _userManager.AddToRolesAsync(employee, model.Roles);

                return RedirectToAction(nameof(Index));
            }

            ViewData["Roles"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), nameof(Role.Name),
                nameof(Role.Name), model.Roles.Select(r => _roleManager.FindByNameAsync(r).Await()));
            return View(model);
        }

        // GET: Dashboard/Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userManager.FindByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var model = EmployeesConverter.Convert(employee);

            ViewData["Roles"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), nameof(Role.Name),
                nameof(Role.Name),
                _userManager.GetRolesAsync(employee).Await().Select(r => _roleManager.FindByNameAsync(r)));
            return View(model);
        }

        // POST: Dashboard/Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            EditEmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var employee = EmployeesConverter.Convert(model);
            var roles = await _userManager.GetRolesAsync(employee);

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(employee);

                    var rolesRemoved = roles.Where(r => !model.Roles.Contains(r, StringComparer.OrdinalIgnoreCase));
                    var rolesAdded = model.Roles.Where(r => !roles.Contains(r, StringComparer.OrdinalIgnoreCase));

                    await _userManager.RemoveFromRolesAsync(employee, rolesRemoved);
                    await _userManager.AddToRolesAsync(employee, rolesAdded);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(model.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Roles"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), nameof(Role.Name),
                nameof(Role.Name), roles.Select(r => _roleManager.FindByNameAsync(r)));
            return View(model);
        }

        // GET: Dashboard/Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userManager.FindByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Dashboard/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(long id)
        {
            return _userManager.FindByIdAsync(id) != null;
        }
    }
}