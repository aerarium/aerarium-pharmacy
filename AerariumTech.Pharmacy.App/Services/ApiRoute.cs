using System;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AerariumTech.Pharmacy.App.Services
{
    public class ApiRoute : Attribute, IRouteTemplateProvider
    {
        public ApiRoute(string name)
        {
            Name = name;
        }

        public string Template
        {
            get
            {
                var controller = Name.StartsWith("api", StringComparison.InvariantCultureIgnoreCase)
                    ? Name.Substring(3)
                    : Name;

                controller = controller.Replace("controller", string.Empty,
                    StringComparison.InvariantCultureIgnoreCase);

                return $"Api/{controller}";
            }
        }

        public int? Order { get; }
        public string Name { get; }
    }
}