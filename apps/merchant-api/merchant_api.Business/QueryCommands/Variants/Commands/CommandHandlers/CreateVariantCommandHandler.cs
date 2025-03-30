using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Business.Dtos.Variants;
using merchant_api.Business.QueryCommands.Variants.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Variants.Commands.CommandHandlers;

public class CreateVariantCommandHandler(
    IValidator<CreateVariantDto> validator,
    IRepository<Variant> variantRepository, 
    IResponseHandlingHelper responseHandlingHelper) 
    : IRequestHandler<CreateVariantCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(
        CreateVariantCommand request, 
        CancellationToken cancellationToken)
    {
        var variantDto = request.Variant;
        var response = await validator.ValidateAsync(variantDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateProductVariantDto>(
            "The operation to create a variant was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());

        var variant = new Variant
        {
            Name = char.ToUpper(request.Variant.Name[0]) + request.Variant.Name[1..].ToLower()
        };
        
        variant  = await variantRepository.AddAsync(variant);;
        return responseHandlingHelper.Created("The variant was added successfully.", variant.Id);
    }
}