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

public class GetAllProductsQueryHandler(IProductRepository productRepository, 
    IWishListRepository wishListRepository,
    ProductService productService, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetAllProductsQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productsLikedIds = new List<Guid>();
        if (request.UserId != null)
        {
            var totalCount = await wishListRepository.GetWishListCountByUserId((Guid)request.UserId);
            var productsLiked = await wishListRepository.GetWishListByUserId((Guid)request.UserId,  1,totalCount);
            productsLikedIds = productsLiked.Select(w => w.ProductId).ToList();
        }
        
        var totalProducts = await productRepository.GetAllAsync(request.Page, request.PageSize);
        var totalProductsDto = totalProducts.Select(e => 
            productService.GetProductDtoByProduct(e, productsLikedIds)).ToList();
        var totalItems = await productRepository.GetCountAsync();

        var productsToDisplay = new PaginatedResponseDto<ProductDto>(totalProductsDto, totalItems, request.Page, request.PageSize);

        return responseHandlingHelper.Ok("Products have been successfully obtained.", productsToDisplay);    
    }
}