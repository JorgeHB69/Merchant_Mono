using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Users;

public class StoreRepository(DbContext context) : BaseRepository<Store>(context), IStoreRepository
{
    public async Task<Store?> GetStoreForSellerAsync(Guid sellerId)
    {
        var stores = await context.Set<Store>().ToListAsync();
        
        return stores.FirstOrDefault(store => 
            store.SellerIds != null && 
            store.SellerIds.Contains(sellerId));
    }
}