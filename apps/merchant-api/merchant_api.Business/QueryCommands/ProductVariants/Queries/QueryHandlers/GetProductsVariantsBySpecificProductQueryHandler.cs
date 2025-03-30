using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Business.QueryCommands.ProductVariants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.ProductVariants.Queries.QueryHandlers;

public class GetProductsVariantsBySpecificProductQueryHandler(
    IProductRepository productRepository,
    IRepository<ProductVariant> productVariantRepository,
    IResponseHandlingHelper responseHandlingHelper) :
    IRequestHandler<GetProductsVariantsBySpecificProductQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetProductsVariantsBySpecificProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.IdProduct);
        if (product == null)
            return responseHandlingHelper.NotFound<Product>("The product with the follow id " + request.IdProduct + " was not found");

        var totalProductVariants = await productVariantRepository.FindAsync(variant => variant.ProductId == request.IdProduct, request.Page, request.PageSize);
        var totalProductVariantDto = totalProductVariants
            .Select(existingProductVariant => new ProductVariantDto
            {
                ProductVariantId = existingProductVariant.Id,
                ProductVariantImage = new ProductVariantImageDto
                {
                    AltText = existingProductVariant.Image?.AltText!,
                    Url = existingProductVariant.Image?.Url!
                },
                ProductId = existingProductVariant.ProductId,
                PriceAdjustment = existingProductVariant.PriceAdjustment,
                StockQuantity = existingProductVariant.StockQuantity,
                Attributes = existingProductVariant.Attributes.Select(currentProductAttribute => new GetProductVariantAttributeDto
                {
                    ProductVariantAttributeId = currentProductAttribute.Id,
                    Name = currentProductAttribute.Variant?.Name!,
                    Value = currentProductAttribute.Value
                }).ToList()
            }).ToList();
        var totalItems = await productVariantRepository.GetCountAsync(variant => variant.ProductId == request.IdProduct);

        var productVariants = new PaginatedResponseDto<ProductVariantDto>(totalProductVariantDto, totalItems, request.Page, request.PageSize);
        return responseHandlingHelper.Ok("Product variants have been successfully obtained.", productVariants);

    }
}