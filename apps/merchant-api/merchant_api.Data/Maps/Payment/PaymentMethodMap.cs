using merchant_api.Data.Models.Concretes.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Payment;

public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethod");
        builder.HasIndex(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.Name).IsRequired();

        builder.HasMany(pm => pm.PaymentTransactions)
            .WithOne(pt => pt.PaymentMethod)
            .HasForeignKey(pt => pt.PaymentMethodId);
    }
}