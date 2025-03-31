using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Inventory;

public class ImageMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Image");
        builder.HasIndex(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();
        builder.Property(i => i.AltText).IsRequired();
        builder.Property(i => i.Url).IsRequired();
        
        builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);
        
        builder.HasOne(i => i.ProductVariant)
            .WithOne(pv => pv.Image)
            .HasForeignKey<ProductVariant>(pv => pv.ImageId);
    }
}