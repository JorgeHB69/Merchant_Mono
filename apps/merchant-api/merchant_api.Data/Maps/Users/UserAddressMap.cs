using merchant_api.Data.Models.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Users;

public class UserAddressMap : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("UserAddress");
        builder.HasIndex(ua => ua.Id);
        builder.Property(ua => ua.Id).ValueGeneratedOnAdd();
        builder.Property(ua => ua.Address).IsRequired();
        builder.Property(ua => ua.City).IsRequired();
        builder.Property(ua => ua.Country).IsRequired();

        builder.HasOne(ua => ua.User)
            .WithOne(u => u.Address)
            .HasForeignKey<UserAddress>(ua => ua.UserId);
    }
}
