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

public class ReduceProductVariantStockByIdCommandHandler(
    IValidator<UpdateProductVariantDto> validator,
    IRepository<ProductVariant> repository,
    ProductVariantService service,
    IResponseHandlingHelper responseHandlingHelper,
    StockThresholdNoticationService stockThresholdNoticationService
) : IRequestHandler<ReduceStockProductVariantByIdCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(ReduceStockProductVariantByIdCommand request, CancellationToken cancellationToken)
    {
        var updateDto = request.VariantStock;
        var existingProductVariant = await repository.GetByIdAsync(updateDto.VariantId);

        if (existingProductVariant is null) return responseHandlingHelper.NotFound<VariantStockDto>($"The product variant with the follow id '{updateDto.VariantId}' was not found.");

        if (existingProductVariant.StockQuantity < updateDto.Quantity) return responseHandlingHelper.BadRequest<VariantStockDto>($"Insufficient stock for the product variant with ID '{updateDto.VariantId}'.");

        if (0 > updateDto.Quantity) return responseHandlingHelper.BadRequest<VariantStockDto>("The amount to be reduced must be 0 or greater.");

        var updateProductVariant = new UpdateProductVariantDto
        {
            Id = updateDto.VariantId,
            StockQuantity = existingProductVariant.StockQuantity - updateDto.Quantity
        };

        var response = await validator.ValidateAsync(updateProductVariant, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<UpdateProductVariantDto>("The operation to update the product variant was not completed, please check the errors.", response.Errors.Select(e => e.ErrorMessage).ToList());

        var productVariantToDisplay = await service.UpdateProductVariant(updateProductVariant, existingProductVariant);

        await stockThresholdNoticationService.NotifyStockThresholdReached([(productVariantToDisplay, updateDto.Quantity)]);
        return responseHandlingHelper.Ok<ProductVariantDto>($"The stock quantity of {updateProductVariant.Id} was updated successfully", productVariantToDisplay);
    }

}