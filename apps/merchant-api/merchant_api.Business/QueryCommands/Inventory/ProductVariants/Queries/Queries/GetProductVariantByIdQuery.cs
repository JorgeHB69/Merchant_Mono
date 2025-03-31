using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Queries.Queries;

public class GetProductVariantByIdQuery(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}