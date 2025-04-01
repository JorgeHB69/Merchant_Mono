using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;

public class GetStoreByIdQuery(Guid id) : IRequest<BaseResponse?>
{
    public Guid Id { get; set; } = id;
}
