using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Images.Queries.Queries;

public class GetImageByIdQuery(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}