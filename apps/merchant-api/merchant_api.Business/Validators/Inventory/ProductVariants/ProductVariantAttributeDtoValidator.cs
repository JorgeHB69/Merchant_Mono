using FluentValidation;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.ProductVariants;

public class ProductVariantAttributeDtoValidator : AbstractValidator<ProductVariantAttributeDto>
{
    public ProductVariantAttributeDtoValidator(IOptions<ValidationSettings> validationSettings)
    {
        var productVariantSettings = validationSettings.Value.ProductVariant;
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Attribute Name is required.")
            .NotNull().WithMessage("Attribute Name cannot be null.")
            .Length(productVariantSettings.AttributeNameMinLenght, productVariantSettings.AttributeNameMaxLenght)
            .WithMessage($"Attribute Name must be between {productVariantSettings.AttributeNameMinLenght} and {productVariantSettings.AttributeNameMaxLenght} characters.");
        
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Attribute Value is required.")
            .NotNull().WithMessage("Attribute Value cannot be null.")
            .Length(productVariantSettings.AttributeValueMinLenght, productVariantSettings.AttributeValueMaxLenght)
            .WithMessage($"Attribute Value must be between {productVariantSettings.AttributeValueMinLenght} and {productVariantSettings.AttributeValueMaxLenght} characters.");
    }
}