using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Queries.Queries;

public class GetAllProductsQuery(Guid? userId, int page, int pageSize) : IRequest<BaseResponse>
{
    public Guid? UserId { get; set; } = userId;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}