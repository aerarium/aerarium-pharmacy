using AerariumTech.Pharmacy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = "Administrator")]
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
