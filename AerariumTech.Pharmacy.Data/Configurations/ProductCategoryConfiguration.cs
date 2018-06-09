using AerariumTech.Pharmacy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AerariumTech.Pharmacy.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(e => new {e.ProductId, e.CategoryId});

            builder.HasOne(e => e.Product)
                .WithMany(e => e.ProductCategories)
                .HasForeignKey(e => e.ProductId);

            builder.HasOne(e => e.Category)
                .WithMany(e => e.ProductCategories)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}