using MediatR;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class UpdateStockCommand(VariantStockDto variantStock) : IRequest<BaseResponse>
{
    public VariantStockDto VariantStock { get; } = variantStock;
}