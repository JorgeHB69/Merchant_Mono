using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;
using merchant_api.Business.Services;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.CommandHandlers;

public class CreateProductVariantCommandHandler(
    IValidator<CreateProductVariantDto> validator,
    IProductRepository productRepository,
    ProductVariantService productVariantService, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<CreateProductVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(CreateProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var productVariantDto = request.ProductVariant;
        var response = await validator.ValidateAsync(productVariantDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateProductVariantDto>(
            "The operation to create a product variant was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());
        
        var product = await productRepository.GetByIdAsync(productVariantDto.ProductId);
        if (product == null)
            return responseHandlingHelper.NotFound<Product>("The product with the follow id " + productVariantDto.ProductId + " was not found");

        var productVariant = await productVariantService.CreateProductVariant(productVariantDto, product.Id);
        return responseHandlingHelper.Created("The product variant was added successfully.", productVariant.Id);
    }
}