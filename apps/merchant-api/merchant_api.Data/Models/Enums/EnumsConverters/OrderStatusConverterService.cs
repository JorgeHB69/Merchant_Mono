using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Data.Models.Enums.EnumsConverters;

public class OrderStatusConverterService
{
    private static readonly Dictionary<OrderStatus, string> OrderStatusMap = new()
    {
        { OrderStatus.Pending, "Pending" },
        { OrderStatus.Confirmed, "Confirmed" },
        { OrderStatus.Shipped, "Shipped" },
        { OrderStatus.Delivered, "Delivered" },
        { OrderStatus.Cancelled, "Cancelled" },
        { OrderStatus.Returned, "Returned" }
    };

    public string ConvertOrderStatusToString(OrderStatus status)
    {
        return OrderStatusMap.GetValueOrDefault(status, "Unknown");
    }
}