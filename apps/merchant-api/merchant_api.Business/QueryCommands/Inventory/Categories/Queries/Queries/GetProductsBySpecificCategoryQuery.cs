using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Queries.Queries;

public class GetProductsBySpecificCategoryQuery(Guid id, Guid? userId, int page, int pageSize): IRequest<BaseResponse>
{
    public Guid? UserId { get; set; } = userId;
    public Guid IdCategory { get; set; } = id;
    public int Page { get; set; } = page; 
    public int PageSize { get; set; } = pageSize;
}