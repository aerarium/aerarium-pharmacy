using static AerariumTech.Pharmacy.Domain.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [Authorize(Roles = Clerk + "," + Manager + "," + Pharmacist)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}