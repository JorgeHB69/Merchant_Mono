using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Bases;

namespace merchant_api.Data.Repositories.Concretes.Payment;

public class OrderItemRepository(PostgresContext context) : BaseRepository<OrderItem>(context);