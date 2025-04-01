using merchant_api.Data.Models.Concretes.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Payment;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        builder.HasIndex(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.OrderNumber).ValueGeneratedOnAdd(); 
        builder.Property(o => o.OrderStatus).IsRequired();
        builder.Property(o => o.TotalPrice).IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(o => o.PaymentTransaction)
            .WithOne(pt => pt.Order)
            .HasForeignKey<PaymentTransaction>(pt => pt.OrderId);
    }
}