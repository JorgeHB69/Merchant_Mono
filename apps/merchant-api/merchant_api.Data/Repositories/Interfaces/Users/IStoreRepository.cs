using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Data.Repositories.Interfaces.Users;

public interface IStoreRepository : IRepository<Store>
{
    Task<Store?> GetStoreForSellerAsync(Guid sellerId);
}