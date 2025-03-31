using MediatR;
using merchant_api.Business.QueryCommands.Products.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.Products.Commands.CommandHandlers;

public class UpdateProducrThresholdCommandHandler(IProductRepository productRepository, IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<UpdateProducrThresholdCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UpdateProducrThresholdCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByIdAsync(request.ProductId);
        if (product == null) return responseHandlingHelper.NotFound<Product>("The product with the follow id " + request.ProductId + " was not found");

        product.LowStockThreshold = request.Threshold;
        await productRepository.UpdateAsync(product);
        return responseHandlingHelper.Ok("The product threshold has been successfully updated.", true);
    }
}
