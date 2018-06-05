using System.Linq;
using AerariumTech.Pharmacy.App.Data;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class ContextExtensions
    {
        public static bool CategoryExists(this PharmacyContext context, long id) =>
            context.Categories.Any(e => e.Id == id);

        public static bool ProductExists(this PharmacyContext context, long id) =>
            context.Products.Any(p => p.Id == id);

        public static bool SupplierExists(this PharmacyContext context, long id) =>
            context.Suppliers.Any(e => e.Id == id);
    }
}