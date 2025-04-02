using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Payment;

public class PaymentTransactionRepository(PostgresContext context) : BaseRepository<PaymentTransaction>(context)
{
    public override async Task<IEnumerable<PaymentTransaction>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(p => p.PaymentMethod)
            .OrderBy(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
