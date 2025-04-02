using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Bases;
using merchant_api.Data.Repositories.Interfaces.Payment;
using merchant_api.Data.Repositories.Interfaces.Payment.QueryExtenssionMethods;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Payment;

public class OrderRepository(PostgresContext context) : BaseRepository<Order>(context), IOrderRepository
{
    public override async Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(o => o.OrderItems)
            .Include(o => o.PaymentTransaction)
            .OrderBy(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersBySpecificUserAsync(Guid userId, 
        OrderFilterParameters parameters, int pageNumber, int pageSize)
    {
        return await DbSet
            .Where(e => 
                e.IsActive && 
                e.UserId == userId)
            .ApplyFilters(parameters)
            .AsSplitQuery()
            .Include(o => o.OrderItems)
            .Include(o => o.PaymentTransaction)
            .OrderBy(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();    
    }
    
    public async Task<int> GetCountBySpecificUserAsync(Guid userId, OrderFilterParameters parameters)
    {
        return await DbSet.Where(e => 
                e.IsActive && 
                e.UserId == userId)
            .ApplyFilters(parameters)
            .CountAsync();    
    }
}