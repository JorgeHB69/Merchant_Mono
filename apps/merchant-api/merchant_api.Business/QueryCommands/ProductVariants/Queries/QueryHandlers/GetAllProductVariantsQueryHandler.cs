using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Business.QueryCommands.ProductVariants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.ProductVariants.Queries.QueryHandlers;

public class GetAllProductVariantsQueryHandler(IRepository<ProductVariant> productVariantRepository,
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetAllProductVariantsQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllProductVariantsQuery request, CancellationToken cancellationToken)
    {
        var totalProductVariants = await productVariantRepository.GetAllAsync(request.Page, request.PageSize);
        var totalProductVariantDto = totalProductVariants.Select(existingProductVariant => new ProductVariantDto
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
                Name = currentProductAttribute.Variant!.Name,
                Value = currentProductAttribute.Value
            }).ToList()
        }).ToList();


        int totalItems = await productVariantRepository.GetCountAsync();


        var variants = new PaginatedResponseDto<ProductVariantDto>(totalProductVariantDto, totalItems, request.Page, request.PageSize);

        return responseHandlingHelper.Ok("Product variants have been successfully obtained.", variants);
    }
}