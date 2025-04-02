using merchant_api.Data.Models.Concretes.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace merchant_api.Data.Maps.Payment;

public class PaymentTransactionMap : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransaction");
        builder.HasIndex(pt => pt.Id);
        builder.Property(pt => pt.Id).ValueGeneratedOnAdd();
        builder.Property(pt => pt.Amount).IsRequired();
        
        builder.HasOne(pt => pt.Order)
            .WithOne(o => o.PaymentTransaction)
            .HasForeignKey<PaymentTransaction>(pt => pt.OrderId);
        
        builder.HasOne(pt => pt.PaymentMethod)
            .WithMany(pm => pm.PaymentTransactions) 
            .HasForeignKey(pt => pt.PaymentMethodId); 

    }
}