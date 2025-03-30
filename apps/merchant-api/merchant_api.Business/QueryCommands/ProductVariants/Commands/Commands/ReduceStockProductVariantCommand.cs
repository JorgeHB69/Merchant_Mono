using MediatR;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class ReduceStockProductVariantCommand(ReduceStockProductVariantDto reduceStockProductVariant) : IRequest<BaseResponse>
{
    public ReduceStockProductVariantDto ReduceStockProductVariant { get; } = reduceStockProductVariant;
}