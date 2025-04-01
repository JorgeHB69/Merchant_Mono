using merchant_api.Data.Models.Bases;
using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Data.Models.Concretes.Payment;

public class Order : BaseEntity
{
    public int OrderNumber { get; set; }  
    public Guid UserId { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public double TotalPrice { get; set; }
    public PaymentTransaction? PaymentTransaction { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}