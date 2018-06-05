using System;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Services
{
    public class DashboardRoute : RouteAttribute
    {
        public DashboardRoute(string template) : base(new Func<string, string>(a => "")(template))
        {
            
        }
    }
}