using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Business.Dtos.Payment.PaymentTransactions;

public class CreatePaymentTransactionDto
{
    public Guid OrderId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public double Amount { get; set; }
    public PaymentStatus TransactionOrderStatus { get; set; } = PaymentStatus.Paid;
}