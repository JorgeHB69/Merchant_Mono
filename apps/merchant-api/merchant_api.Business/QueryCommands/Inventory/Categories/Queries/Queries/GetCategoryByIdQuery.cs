using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Queries.Queries;

public class GetCategoryByIdQuery(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}