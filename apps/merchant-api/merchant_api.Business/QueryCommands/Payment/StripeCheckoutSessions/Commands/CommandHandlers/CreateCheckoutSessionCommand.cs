using MediatR;
using merchant_api.Business.Dtos.Payment.CheckoutSessions;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.StripeCheckoutSessions.Commands.CommandHandlers;

public class CreateCheckoutSessionCommand(CheckoutSessionRequestDto checkoutSessionRequestDto) : IRequest<BaseResponse>
{
    public List<ShoppingCartItemDto> ShoppingCartList { get; set; } = checkoutSessionRequestDto.ShoppingCartItems;
    public CustomerDTO Customer { get; set; } = checkoutSessionRequestDto.Customer;
}
