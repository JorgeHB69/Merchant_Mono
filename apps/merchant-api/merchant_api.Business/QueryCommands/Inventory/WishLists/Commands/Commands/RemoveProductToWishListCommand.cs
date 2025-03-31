using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.WishLists.Commands.Commands;

public class RemoveProductToWishListCommand(Guid userId, Guid productId) : IRequest<BaseResponse>
{
    public Guid UserId { get; set; } = userId;
    public Guid ProductId { get; set; } = productId;
}