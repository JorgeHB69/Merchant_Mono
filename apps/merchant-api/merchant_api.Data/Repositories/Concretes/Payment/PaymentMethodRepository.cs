using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Payment;

public class PaymentMethodRepository(PostgresContext context) : BaseRepository<PaymentMethod>(context)
{
    public override async Task<PaymentMethod> AddAsync(PaymentMethod entity)
    {
        var existingPaymentMethod = await DbSet.FirstOrDefaultAsync(c => c.Name == entity.Name && c.IsActive);
        if (existingPaymentMethod != null) return existingPaymentMethod; 
        
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
}