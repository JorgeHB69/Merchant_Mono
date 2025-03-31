using MediatR;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Variants.Commands.Commands;

public class CreateVariantCommand(CreateVariantDto variant) : IRequest<BaseResponse>
{
    public CreateVariantDto Variant { get; } = variant;
}