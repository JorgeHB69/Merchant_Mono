using merchant_api.Data.Models.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Users;

public class StoreMap : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Store");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .IsRequired();
        builder.Property(s => s.Description)
            .IsRequired();
        builder.Property(s => s.Address)
            .IsRequired();
        builder.Property(s => s.PhoneNumber)
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithOne(u => u.Store)
            .HasForeignKey<Store>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.SellerIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => Guid.Parse(id))
                    .ToList(),
                new ValueComparer<ICollection<Guid>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );
    }
}