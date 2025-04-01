using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;

public class GetStoreSellersQuery(Guid storeId) : IRequest<List<BaseResponse>>
{
    public Guid StoreId { get; set; } = storeId;
}
