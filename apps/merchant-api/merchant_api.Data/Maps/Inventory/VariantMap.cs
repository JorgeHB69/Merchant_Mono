using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Inventory;

public class VariantMap : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
        builder.ToTable("Variant");
        builder.HasKey(v => v.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(v => v.Id).ValueGeneratedOnAdd();
        
        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(v => v.ProductAttributes)
            .WithOne(pa => pa.Variant)
            .HasForeignKey(pa => pa.VariantId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}