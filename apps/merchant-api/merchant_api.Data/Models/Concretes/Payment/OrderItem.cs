using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Payment;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public int DiscountPercent { get; set; }
    public double TotalPrice { get; set; }
    public Order? Order { get; set; }
}
