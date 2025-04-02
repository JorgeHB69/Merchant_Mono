namespace merchant_api.Business.Dtos.Payment.CheckoutSessions;

public class CheckoutSessionRequestDto
{
    public List<ShoppingCartItemDto> ShoppingCartItems { get; set; } = [];
    public CustomerDTO Customer { get; set; } = new();
}