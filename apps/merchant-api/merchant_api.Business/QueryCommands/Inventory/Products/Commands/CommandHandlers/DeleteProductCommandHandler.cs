using MediatR;
using merchant_api.Business.QueryCommands.Products.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.Products.Commands.CommandHandlers;

public class DeleteProductCommandHandler(IProductRepository productRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<DeleteProductCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product == null) return responseHandlingHelper.NotFound<Category>($"The product with the follow id '{request.Id}' was not found.");

        var response = await productRepository.DeleteAsync(request.Id);
        return responseHandlingHelper.Ok("The product has been successfully deleted.", response); 
    }
}