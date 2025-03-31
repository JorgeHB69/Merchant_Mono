using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Queries.Queries;

public class GetProductsByStoreIdQuery(Guid id, int page, int pageSize, ProductFilteringQueryParams queryParams) : IRequest<BaseResponse>
{
    public Guid StoreId { get; set; } = id;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
    public ProductFilteringQueryParams QueryParams { get; set; } = queryParams;
}