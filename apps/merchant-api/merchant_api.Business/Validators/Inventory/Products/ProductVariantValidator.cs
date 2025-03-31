using FluentValidation;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.Validators.Inventory.ProductVariants;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.Products;

public class ProductVariantValidator : AbstractValidator<ProductVariantForCreateDto>
{
    public ProductVariantValidator(IOptions<ValidationSettings> validationSettings)
    {
        var productVariantSettings = validationSettings.Value.ProductVariant;
        
        RuleFor(pv => pv.Image)
            .SetValidator(new ProductVariantImageDtoValidator(validationSettings)) 
            .When(x => x.Image != null);

        RuleFor(x => x.PriceAdjustment)
            .NotNull().WithMessage("Price Adjustment cannot be null.");

        RuleFor(x => x.StockQuantity)
            .NotNull().WithMessage("Stock Quantity cannot be null.")
            .GreaterThan(productVariantSettings.StockQuantityMin)
            .WithMessage($"The Stock Quantity must be greater than {productVariantSettings.StockQuantityMin}.");

        RuleFor(x => x.Attributes)
            .NotNull().WithMessage("Attributes is required")
            .Must(o => o is { Count: >= 1 }).WithMessage("Product Variant must have at least one attribute.");  
        
        RuleForEach(x => x.Attributes)
            .SetValidator(new ProductVariantAttributeDtoValidator(validationSettings))
            .When(x => x.Attributes.Count != 0);
    }
}