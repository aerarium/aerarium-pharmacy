using System;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Services
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute(string template) : base(RenameRoute(template))
        {
        }

        private static string RenameRoute(string template)
        {
            var entity = template
                .Replace("api", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("controller", string.Empty, StringComparison.OrdinalIgnoreCase);

            return $"Api/{entity}";
        }
    }
}