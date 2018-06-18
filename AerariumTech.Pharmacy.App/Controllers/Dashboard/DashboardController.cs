using AerariumTech.Pharmacy.App.Services;
using AerariumTech.Pharmacy.Data;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [AdminOnly]
    public class DashboardController : Controller
    {
        private readonly PharmacyContext _context;

        public DashboardController(PharmacyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
