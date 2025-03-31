using FluentValidation;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator(IOptions<ValidationSettings> validationSettings)
    {
        var categorySettings = validationSettings.Value.Category;
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name cannot be null.")
            .Length(categorySettings.CategoryNameMinLength, categorySettings.CategoryNameMaxLength)
            .WithMessage($"Name must be between {categorySettings.CategoryNameMinLength} and {categorySettings.CategoryNameMaxLength} characters.");
    }
}