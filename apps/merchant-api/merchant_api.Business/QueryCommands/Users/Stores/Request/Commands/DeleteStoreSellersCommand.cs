using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Stores.Request.Commands;

public class DeleteStoreSellersCommand : IRequest<BaseResponse>
{
    public Guid StoreId { get; set; }
    public Guid SellerId { get; set; } = new();

    public DeleteStoreSellersCommand(Guid storeId, Guid sellerId)
    {
        StoreId = storeId;
        SellerId = sellerId;
    }
}