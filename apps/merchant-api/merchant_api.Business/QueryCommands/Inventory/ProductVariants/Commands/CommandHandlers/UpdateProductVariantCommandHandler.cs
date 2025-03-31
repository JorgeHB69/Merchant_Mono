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

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.CommandHandlers;

public class UpdateProductVariantCommandHandler(
    IValidator<UpdateProductVariantDto> validator,
    IRepository<ProductVariant> productVariantRepository,
    ProductVariantService productVariantService, 
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<UpdateProductVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UpdateProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var updateDto = request.ProductVariant;
        if (updateDto.Id != null)
        {
            var existingProductVariant = await productVariantRepository.GetByIdAsync(updateDto.Id.Value);
            if (existingProductVariant == null) return responseHandlingHelper.NotFound<ProductVariant>(
                $"The product variant with the follow id '{updateDto.Id}' was not found.");
        
            var response = await validator.ValidateAsync(updateDto, cancellationToken);
            if (!response.IsValid) return responseHandlingHelper.BadRequest<UpdateProductVariantDto>(
                "The operation to update the product variant was not completed, please check the errors.", 
                response.Errors.Select(e => e.ErrorMessage).ToList());
        
            var productVariantToDisplay = await productVariantService.UpdateProductVariant(updateDto, existingProductVariant);
            return responseHandlingHelper.Ok("The product variant has been successfully updated.", productVariantToDisplay);
        }

        return responseHandlingHelper
            .BadRequest<UpdateProductVariantDto>("The operation to update the variant wasn't completed");
    }
}