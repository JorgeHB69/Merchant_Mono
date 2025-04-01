namespace merchant_api.Business.Dtos.Payment.CheckoutSessions;

public class CustomerDTO
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
}
