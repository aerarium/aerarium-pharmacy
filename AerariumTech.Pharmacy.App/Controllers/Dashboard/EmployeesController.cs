using AerariumTech.Pharmacy.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Controllers.Dashboard
{
    [DashboardRoute]
    public class EmployeesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}