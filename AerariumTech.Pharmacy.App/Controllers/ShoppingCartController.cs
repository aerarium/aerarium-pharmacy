﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.Models.ShippingRateViewModels;
using AerariumTech.Pharmacy.Models.ShoppingCartViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AerariumTech.Pharmacy.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ShoppingCartController> _logger;

        [TempData]
        public string Status { get; set; }
        
        public ShoppingCartController(PharmacyContext context, UserManager<User> userManager,
            ILogger<ShoppingCartController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [NonAction]
        protected virtual async Task<IEnumerable<SelectShippingRateViewModel>> GetShippingRateOptionsAsync()
        {
            var shippingRates = _context.ShippingRates
                .Include(s => s.StateProvince);

            var options = await shippingRates.Select(s => new SelectShippingRateViewModel
            {
                Value = s.Id,
                Text = $"{s.StateProvince.Name} - R${s.Price}"
            }).OrderBy(s => s.Text).ToListAsync();

            options.Add(new SelectShippingRateViewModel
            {
                Value = null,
                Text = "Retirar na loja"
            });

            return options;
        }

        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.GetShoppingCart();
            await UpdatePrices(cart);
            var productsId = cart.Items.Select(c => c.ProductId);

            var model = new ShoppingCartViewModel(_context.Products.Where(p => productsId.Contains(p.Id)).AsNoTracking()
                .Select(
                    p => new ShoppingCartItemViewModel
                    {
                        ProductId = p.Id,
                        Quantity = cart.Items.SingleOrDefault(c => c.ProductId == p.Id).Quantity,
                        Price = p.ActualPrice,
                        ProductName = p.Name,
                        ProductPicture = p.PathToPicture
                    }));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddToCartViewModel model)
        {
            var cart = HttpContext.GetShoppingCart();
            var item = cart.Items.SingleOrDefault(c => c.ProductId == model.ProductId) ??
                       throw new NullReferenceException(nameof(model) + " must be in cart already");
            var qtdStock = await _context.Batches.GetAmountInStockAsync(model.ProductId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.ProductId);

            var status = $"Unable to add {product.Name} to cart!";

            if (qtdStock > 0)
            {
                var amount = model.Quantity;
                status = $"{amount} units of {product.Name} added to cart.";

                if (model.Quantity >= qtdStock)
                {
                    amount = qtdStock;
                    status = $"Could only add {amount} {product.Name} to cart.";
                }

                item.Quantity = amount;

                await UpdatePrices(cart);

                HttpContext.SetShoppingCart(cart);
            }

            _logger.LogInformation(status);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(long productId)
        {
            var cart = HttpContext.GetShoppingCart();

            var item = cart.Items.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            await UpdatePrices(cart);

            HttpContext.SetShoppingCart(cart);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(AddToCartViewModel model)
        {
            var cart = HttpContext.GetShoppingCart();
            var qtdStock = await _context.Batches.GetAmountInStockAsync(model.ProductId);
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == model.ProductId);

            var status = $"Unable to add {product.Name} to cart!";

            if (qtdStock > 0)
            {
                var amount = model.Quantity;
                status = $"{amount} units of {product.Name} added to cart.";

                if (model.Quantity > qtdStock)
                {
                    amount = qtdStock;
                    status = $"Could only add {amount} {product.Name} to cart.";
                }

                var item = cart.Items.FirstOrDefault(c => c.ProductId == product.Id);
                if (item == null)
                {
                    cart.Items.Add(new ShoppingCartItem
                    {
                        ProductId = product.Id,
                        Quantity = amount
                    });
                }
                else
                {
                    item.Quantity += amount;
                }

                await UpdatePrices(cart);

                HttpContext.SetShoppingCart(cart);
            }

            _logger.LogInformation(status);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = HttpContext.GetShoppingCart();

            if (!cart.Items.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var options = await GetShippingRateOptionsAsync();

            ViewData["ShippingRates"] = new SelectList(options, nameof(SelectShippingRateViewModel.Value),
                nameof(SelectShippingRateViewModel.Text));
            ViewData["PaymentModes"] = new SelectList(_context.PaymentModes, nameof(PaymentMode.Id),
                nameof(PaymentMode.Description));

            return View();
        }

        // TODO NOT WORKING
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var sale = ShoppingCartConverter.Convert(model);
                sale.Customer = await _userManager.GetUserAsync(User);
                sale.IssuedAt = now;
                sale.Payment.DateOfExpiration = now.AddMonths(6);

                await _context.Sales.AddAsync(sale);

                await _context.SaveChangesAsync();

                HttpContext.DeleteShoppingCart();

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var options = await GetShippingRateOptionsAsync();

            ViewData["ShippingRates"] = new SelectList(options, nameof(SelectShippingRateViewModel.Value),
                nameof(SelectShippingRateViewModel.Text), model.ShippingRateId);
            ViewData["PaymentModes"] = new SelectList(_context.PaymentModes, nameof(PaymentMode.Id),
                nameof(PaymentMode.Description), model.PaymentModeId);

            return View(model);
        }

        #region Helpers

        protected virtual async Task<ShoppingCart> UpdatePrices(ShoppingCart cart)
        {
            var tasks = cart.Items.Select(async sci => new ShoppingCartItem
            {
                ProductId = sci.ProductId,
                Quantity = sci.Quantity,
                Price = (await _context.Products.SingleOrDefaultAsync(p => p.Id == sci.ProductId)).ActualPrice
            }); // so we can update prices

            cart.Items = await Task.WhenAll(tasks);

            return cart;
        }

        #endregion
    }
}