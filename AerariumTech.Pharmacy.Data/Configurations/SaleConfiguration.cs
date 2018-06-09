using AerariumTech.Pharmacy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AerariumTech.Pharmacy.Data.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasOne(e => e.Customer)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.CustomerId);
            
            builder.HasOne(e => e.Seller)
                .WithMany(e => e.Sales)
                .HasForeignKey(e => e.SellerId);
        }
    }
}