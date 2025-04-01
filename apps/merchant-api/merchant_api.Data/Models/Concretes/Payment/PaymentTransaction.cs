using merchant_api.Data.Models.Bases;
using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Data.Models.Concretes.Payment;

public class PaymentTransaction : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public double Amount { get; set; }
    public Order Order { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus TransactionOrderStatus { get; set; } = PaymentStatus.Paid;
}