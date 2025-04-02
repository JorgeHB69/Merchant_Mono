using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Business.Dtos.Payment.Orders;

public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public List<CreateOrderItemDto> Items { get; set; } = [];
}