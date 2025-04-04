using FluentValidation;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.Variants;

public class CreateVariantValidator : AbstractValidator<CreateVariantDto>
{
    public CreateVariantValidator(IOptions<ValidationSettings> validationSettings)
    {
        var variantSettings = validationSettings.Value.Variant;
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name cannot be null.")
            .Length(variantSettings.VariantNameMinLength, variantSettings.VariantNameMaxLength)
            .WithMessage($"Name must be between {variantSettings.VariantNameMinLength} and {variantSettings.VariantNameMaxLength} characters.");
    }
}