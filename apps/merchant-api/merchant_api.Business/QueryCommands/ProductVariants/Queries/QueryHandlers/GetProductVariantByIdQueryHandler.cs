using MediatR;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Business.QueryCommands.ProductVariants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.ProductVariants.Queries.QueryHandlers;

public class GetProductVariantByIdQueryHandler(IRepository<ProductVariant> productVariantRepository, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetProductVariantByIdQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var existingProductVariant = await productVariantRepository.GetByIdAsync(request.Id);
        if (existingProductVariant == null)
            return responseHandlingHelper.NotFound<ProductVariantDto>("The product variant with the follow id " + request.Id + " was not found");
        
        var variantImageDto = new ProductVariantImageDto { AltText = existingProductVariant.Image?.AltText!, Url = existingProductVariant.Image?.Url! };
        return responseHandlingHelper.Ok("The product variant has been successfully obtained.", new ProductVariantDto
        {
            ProductVariantId = existingProductVariant.Id,
            ProductVariantImage = variantImageDto,
            ProductId = existingProductVariant.ProductId,
            PriceAdjustment = existingProductVariant.PriceAdjustment,
            StockQuantity = existingProductVariant.StockQuantity,
            Attributes = existingProductVariant.Attributes.Select(currentProductAttribute => new GetProductVariantAttributeDto
            {
                ProductVariantAttributeId = currentProductAttribute.Id,
                Name = currentProductAttribute.Variant?.Name!,
                Value = currentProductAttribute.Value
            }).ToList()
        });
    }
}