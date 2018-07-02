using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using static AerariumTech.Pharmacy.Domain.Role;
using AerariumTech.Pharmacy.Models.ProductsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = Clerk + "," + Manager + "," + Pharmacist)]
    [DashboardRoute]
    public class ProductsController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly string _wwwRoot;
        private readonly string _relativeImagesFolder;

        public ProductsController(PharmacyContext context, IHostingEnvironment environment)
        {
            _context = context;

            _wwwRoot = environment.WebRootPath;
            _relativeImagesFolder = "images";

            var absoluteImagesFolder = Path.Combine(_wwwRoot, _relativeImagesFolder);
            if (!Directory.Exists(absoluteImagesFolder))
            {
                Directory.CreateDirectory(absoluteImagesFolder);
            }
        }

        // GET: Dashboard/Products
        public async Task<IActionResult> Index()
        {
            var pharmacyContext = _context.Products.Include(p => p.Supplier);
            return View(await pharmacyContext.ToListAsync());
        }

        // GET: Dashboard/Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Supplier).SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Dashboard/Products/Create
        public IActionResult Create()
        {
            ViewData["Categories"] =  new MultiSelectList(_context.Categories.OrderBy(c => c.Name), nameof(Category.Id), nameof(Category.Name));
            ViewData["Suppliers"] = new SelectList(_context.Suppliers.OrderBy(s => s.Name), nameof(Supplier.Id), nameof(Supplier.Name));
            return View();
        }

        // POST: Dashboard/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            // [Bind("Name,Description,Price,PriceWithDiscount,SerialCode,PathToPicture,SupplierId")]
            CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = ProductsConverter.Convert(model);

                if (model?.PictureFile?.Length > 0)
                {
                    // use Guid to create a new name for the file
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(model.PictureFile.FileName);

                    using (var fileStream = new FileStream(Path.Combine(_wwwRoot, _relativeImagesFolder, imageName), FileMode.Create))
                    {
                        await model.PictureFile.CopyToAsync(fileStream);

                        product.PathToPicture = Path.DirectorySeparatorChar + Path.Combine(_relativeImagesFolder, imageName); // Save only relative path to db
                    }
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Categories"] = new MultiSelectList(_context.Categories.OrderBy(c => c.Name), nameof(Category.Id), nameof(Category.Name));
            ViewData["Suppliers"] = new SelectList(_context.Suppliers.OrderBy(s => s.Name), nameof(Supplier.Id), nameof(Supplier.Name),
                model.SupplierId);
            return View(model);
        }

        // GET: Dashboard/Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var model = ProductsConverter.Convert(product);

            ViewData["Categories"] = new MultiSelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            ViewData["Suppliers"] = new SelectList(_context.Suppliers, nameof(Supplier.Id), nameof(Supplier.Name),
                model.SupplierId);

            return View(model);
        }

        // POST: Dashboard/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = ProductsConverter.Convert(model);
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProductExists(model.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Categories"] = new MultiSelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            ViewData["Suppliers"] = new SelectList(_context.Suppliers, nameof(Supplier.Id), nameof(Supplier.Name),
                model.SupplierId);
            return View(model);
        }

        // GET: Dashboard/Products/Delete/5
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Dashboard/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}