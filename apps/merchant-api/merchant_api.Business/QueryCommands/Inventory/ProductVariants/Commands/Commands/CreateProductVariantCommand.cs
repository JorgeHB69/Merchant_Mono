using MediatR;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class CreateProductVariantCommand(CreateProductVariantDto productVariant) : IRequest<BaseResponse>
{
    public CreateProductVariantDto ProductVariant { get; } = productVariant;
}