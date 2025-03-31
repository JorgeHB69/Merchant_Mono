using MediatR;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.QueryCommands.Products.Queries.Queries;
using merchant_api.Business.Services;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.Products.Queries.QueryHandlers;

public class GetProductByIdQueryHandler(
IProductRepository productRepository, 
    ProductService productService, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetProductByIdQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetByIdAsync(request.Id);
        if (existingProduct == null)
            return responseHandlingHelper.NotFound<ProductDto>("The product with the follow id " + request.Id + " was not found");

        var productDto = productService.GetProductDtoByProduct(existingProduct, []);
        return responseHandlingHelper.Ok("The product has been successfully obtained.", productDto);
    }
}