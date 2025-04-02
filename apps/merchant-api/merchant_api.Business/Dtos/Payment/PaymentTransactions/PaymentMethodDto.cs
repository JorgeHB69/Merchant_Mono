namespace merchant_api.Business.Dtos.Payment.PaymentTransactions;

public class PaymentMethodDto
{
    public Guid PaymentMethodId { get; set; }
    public string Name { get; set; } = string.Empty;
}