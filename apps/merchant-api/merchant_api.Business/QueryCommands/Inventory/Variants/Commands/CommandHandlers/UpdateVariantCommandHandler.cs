using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Business.QueryCommands.Variants.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Variants.Commands.CommandHandlers;

public class UpdateVariantCommandHandler(
    IValidator<UpdateVariantDto> validator,
    IRepository<Variant> variantRepository, 
    IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<UpdateVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
    {
        var variantDto = request.Variant;
        var variantToUpdate = await variantRepository.GetByIdAsync(variantDto.Id);
        if (variantToUpdate == null) return responseHandlingHelper.NotFound<Category>(
            $"The category with the follow id '{variantDto.Id}' was not found.");

        var response = await validator.ValidateAsync(variantDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<UpdateVariantDto>(
            "The operation to update the variant was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());
        
        variantToUpdate.Name = variantDto.Name ?? variantToUpdate.Name;
        variantToUpdate.IsActive = variantDto.IsActive ?? variantToUpdate.IsActive;

        await variantRepository.UpdateAsync(variantToUpdate);
        var variantToDisplay = new VariantDto
        {
            Id = variantToUpdate.Id,
            Name = variantToUpdate.Name,
            IsActive = variantToUpdate.IsActive,
            values = variantToUpdate.ProductAttributes.Select(attribute => new ValueDto
            {
                Id = attribute.Id,
                Name = attribute.Value
            }).ToList()
        };
        
        return responseHandlingHelper.Ok("The variant has been successfully updated.", variantToDisplay);
    }
}