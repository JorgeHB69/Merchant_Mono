using FluentValidation;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.ProductVariants;

public class UpdateProductVariantValidator : AbstractValidator<UpdateProductVariantDto>
{
    public UpdateProductVariantValidator(IOptions<ValidationSettings> validationSettings)
    {
        var productVariantSettings = validationSettings.Value.ProductVariant;
        
        RuleFor(pv => pv.Id)
            .NotNull().WithMessage("Product Variant ID cannot be null.")
            .NotEmpty().WithMessage("Product Variant ID is required.");
    
    }
}