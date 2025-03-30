using MediatR;
using merchant_api.Business.QueryCommands.Images.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Images.Commands.CommandHandlers;

public class DeleteImageCommandHandler(IRepository<Image> imageRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<DeleteImageCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await imageRepository.GetByIdAsync(request.Id);
        if (image == null) return responseHandlingHelper.NotFound<Image>($"The image with the follow id '{request.Id}' was not found.");

        var response = await imageRepository.DeleteAsync(request.Id);
        return responseHandlingHelper.Ok("The image has been successfully deleted.", response);
    }
}