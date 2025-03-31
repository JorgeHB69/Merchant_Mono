using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Queries.Queries;

public class GetProductsVariantsBySpecificProductQuery(Guid id, int page, int pageSize) : IRequest<BaseResponse>
{
    public Guid IdProduct { get; set; } = id;
    public int Page { get; set; } = page; 
    public int PageSize { get; set; } = pageSize;
}