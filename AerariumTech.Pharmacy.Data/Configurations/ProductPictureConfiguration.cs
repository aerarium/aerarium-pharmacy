using AerariumTech.Pharmacy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AerariumTech.Pharmacy.Data.Configurations
{
    public class ProductPictureConfiguration : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.HasKey(e => new {e.ProductId, e.Picture});
    
            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductPictures)
                .HasForeignKey(e => e.ProductId);
        }
    }
}