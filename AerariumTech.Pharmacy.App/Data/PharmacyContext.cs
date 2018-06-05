using AerariumTech.Pharmacy.App.Data.Configurations;
using AerariumTech.Pharmacy.App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace AerariumTech.Pharmacy.App.Data
{
    // ReSharper disable once ClassNeverInstantiated.Global
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
        public DbSet<ProductPicture> ProductPictures { get; set; }
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
                .ApplyConfiguration(new ProductCategoryConfiguration())
                .ApplyConfiguration(new ProductPictureConfiguration())
                .ApplyConfiguration(new SaleInvoiceConfiguration());
        }
    }
}