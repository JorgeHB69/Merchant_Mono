using merchant_api.Data.Models.Concretes.Payment;

namespace merchant_api.Data.Repositories.Interfaces.Payment;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IEnumerable<Order>> GetAllOrdersBySpecificUserAsync(Guid userId, OrderFilterParameters parameters, int pageNumber, int pageSize);
    public Task<int> GetCountBySpecificUserAsync(Guid userId, OrderFilterParameters parameters);
}