using MediatR;
using merchant_api.Business.Dtos.Variants;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Variants.Commands.Commands;

public class UpdateVariantCommand(UpdateVariantDto variant) : IRequest<BaseResponse>
{
    public UpdateVariantDto Variant { get; } = variant;
}