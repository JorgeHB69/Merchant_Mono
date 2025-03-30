using MediatR;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class ReduceStockProductVariantByIdCommand(VariantStockDto variantStock) : IRequest<BaseResponse>
{
    public VariantStockDto VariantStock { get; } = variantStock;
}