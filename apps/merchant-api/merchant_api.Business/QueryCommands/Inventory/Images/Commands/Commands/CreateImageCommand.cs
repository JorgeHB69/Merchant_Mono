using MediatR;
using merchant_api.Business.Dtos.Inventory.Images;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Images.Commands.Commands;

public class CreateImageCommand(CreateImageDto image) : IRequest<BaseResponse>
{
    public CreateImageDto Image { get; } = image;
}