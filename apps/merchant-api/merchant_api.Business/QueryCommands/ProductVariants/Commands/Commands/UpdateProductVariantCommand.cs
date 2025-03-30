using MediatR;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class UpdateProductVariantCommand(UpdateProductVariantDto productVariant) : IRequest<BaseResponse>
{
    public UpdateProductVariantDto ProductVariant { get; } = productVariant;
}