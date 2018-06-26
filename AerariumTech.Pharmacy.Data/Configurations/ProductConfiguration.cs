using AerariumTech.Pharmacy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AerariumTech.Pharmacy.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.HasIndex(e => e.SerialCode)
                .IsUnique();
        }
    }
}