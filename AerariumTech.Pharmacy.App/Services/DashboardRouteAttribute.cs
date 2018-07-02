using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Services
{
    public class DashboardRouteAttribute : RouteAttribute
    {
        public DashboardRouteAttribute() : base("Dashboard/[controller]/[action]/{id?}")
        {
        }
    }
}