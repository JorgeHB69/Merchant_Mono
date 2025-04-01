using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;

public class GetStoreForSellerQuery(Guid sellerId) : IRequest<BaseResponse?>
{
    public Guid SellerId { get; set; } = sellerId;
}
