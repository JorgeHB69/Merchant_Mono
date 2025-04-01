using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Payment;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
}