using merchant_api.Data.Models.Concretes.Inventory;

namespace merchant_api.Data.Repositories.Interfaces.Inventory;

public interface IWishListRepository : IRepository<WishList>
{
    public Task<IEnumerable<WishList>> GetWishListByUserId(Guid userId, int pageNumber, int pageSize, 
        ProductFilteringQueryParams? queryParams = null);
    public Task<int> GetWishListCountByUserId(Guid userId, ProductFilteringQueryParams? queryParams = null);
    public Task<WishList?> GetByUserIdAndProductIdAsync(Guid userId, Guid productId);
}