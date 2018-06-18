using AerariumTech.Pharmacy.Data.Configurations;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.Data
{
    public class PharmacyContext
        : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public PharmacyContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }
        public DbSet<ShippingRate> ShippingRates { get; set; }
        public DbSet<StateProvince> StateProvinces { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName =
                    entity.Relational().TableName.Replace("AspNet", string.Empty);
            }

            builder
                .ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new ProductCategoryConfiguration())
                .ApplyConfiguration(new SaleConfiguration())
                .ApplyConfiguration(new SaleInvoiceConfiguration());
        }
    }
}