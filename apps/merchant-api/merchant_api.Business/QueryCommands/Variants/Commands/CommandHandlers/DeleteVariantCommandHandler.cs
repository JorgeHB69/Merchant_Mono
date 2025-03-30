using MediatR;
using merchant_api.Business.QueryCommands.Variants.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Variants.Commands.CommandHandlers;

public class DeleteVariantCommandHandler(IRepository<Variant> variantRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<DeleteVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = await variantRepository.GetByIdAsync(request.Id);
        if (variant == null) return responseHandlingHelper.NotFound<Category>($"The variant with the follow id '{request.Id}' was not found.");

        var response = await variantRepository.DeleteAsync(request.Id);
        return responseHandlingHelper.Ok("The variant has been successfully deleted.", response);
    }
}