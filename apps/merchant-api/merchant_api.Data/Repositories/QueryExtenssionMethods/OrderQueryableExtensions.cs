using merchant_api.Data.Models.Concretes.Payment;

namespace merchant_api.Data.Repositories.Interfaces.Payment.QueryExtenssionMethods;

public static class OrderQueryableExtensions
{
    public static IQueryable<Order> ApplyFilters(
        this IQueryable<Order> query,
        OrderFilterParameters parameters)
    {
        return query
            .Where(o => !parameters.StartDate.HasValue || o.CreatedAt.Date >= parameters.StartDate.Value.Date)
            .Where(o => !parameters.EndDate.HasValue || o.CreatedAt.Date <= parameters.EndDate.Value.Date)
            .Where(o => !parameters.MinPrice.HasValue || o.TotalPrice >= parameters.MinPrice.Value)
            .Where(o => !parameters.MaxPrice.HasValue || o.TotalPrice <= parameters.MaxPrice.Value)
            .Where(o => !parameters.Status.HasValue || o.OrderStatus == parameters.Status.Value);
    }
}