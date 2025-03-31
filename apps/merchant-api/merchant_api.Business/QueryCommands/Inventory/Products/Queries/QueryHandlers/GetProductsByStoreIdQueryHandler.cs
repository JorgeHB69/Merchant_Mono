using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.QueryCommands.Products.Queries.Queries;
using merchant_api.Business.Services;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.Products.Queries.QueryHandlers;
public class GetProductsByStoreIdQueryHandler(IProductRepository productRepository, ProductService productService,
    IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<GetProductsByStoreIdQuery, BaseResponse>
{

    public async Task<BaseResponse> Handle(GetProductsByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByStoreId(request.StoreId, request.Page, request.PageSize, request.QueryParams);

        var productsDto = products.Select(e =>
            productService.GetProductDtoByProduct(e, [])).ToList();

        var totalItems = await productRepository.GetCountProductsByStoreId(request.StoreId, request.QueryParams);

        var productsToDisplay = new PaginatedResponseDto<ProductDto>(productsDto, totalItems, request.Page, request.PageSize);

        return responseHandlingHelper.Ok("Products have been successfully obtained.", productsToDisplay);

    }
}
