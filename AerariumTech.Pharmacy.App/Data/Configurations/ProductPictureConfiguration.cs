using AerariumTech.Pharmacy.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AerariumTech.Pharmacy.App.Data.Configurations
{
    public class ProductPictureConfiguration : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        { // TODO is this right?
            // anyway maybe it doesn't matter, 'cause it requires a PK
            builder.HasKey(e => new {e.ProductId, e.Picture});
    
            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductPictures)
                .HasForeignKey(e => e.ProductId);
        }
    }
}
