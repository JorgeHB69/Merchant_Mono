using merchant_api.Data.Models.Concretes.Inventory;

namespace merchant_api.Data.Repositories.Interfaces.Inventory;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByStoreId(Guid id, int pageNumber, int pageSize, ProductFilteringQueryParams queryParams);
    Task<int> GetCountProductsByStoreId(Guid id, ProductFilteringQueryParams queryParams);
}