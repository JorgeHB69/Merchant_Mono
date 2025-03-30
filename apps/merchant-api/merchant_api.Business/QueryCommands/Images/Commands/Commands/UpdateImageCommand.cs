using MediatR;
using merchant_api.Business.Dtos.Images;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Images.Commands.Commands;

public class UpdateImageCommand(UpdateImageDto image) : IRequest<BaseResponse>
{
    public UpdateImageDto Image { get; } = image;
}