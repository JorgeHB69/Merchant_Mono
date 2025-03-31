using MediatR;
using merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.CommandHandlers;

public class DeleteProductVariantCommandHandler(
    IRepository<ProductVariant> productVariantRepository, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<DeleteProductVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        var productVariant = await productVariantRepository.GetByIdAsync(request.Id);
        if (productVariant == null) return responseHandlingHelper.NotFound<ProductVariant>($"The product variant with the follow id '{request.Id}' was not found.");

        var response = await productVariantRepository.DeleteAsync(request.Id);
        return responseHandlingHelper.Ok("The product variant has been successfully deleted.", response);
    }
}