﻿using FluentValidation;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Inventory.Products;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator(IOptions<ValidationSettings> validationSettings)
    {
        var productSettings = validationSettings.Value.Product;
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Product Name is required.")
            .NotNull().WithMessage("Product Name cannot be null.")
            .Length(productSettings.ProductNameMinLength, productSettings.ProductNameMaxLength)
            .WithMessage($"Product Name must be between {productSettings.ProductNameMinLength} and {productSettings.ProductNameMaxLength} characters.");
        
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description cannot be null.")
            .Length(productSettings.ProductDescriptionMinLength, productSettings.ProductDescriptionMaxLenght)
            .WithMessage($"Description must be between {productSettings.ProductDescriptionMinLength} and {productSettings.ProductDescriptionMaxLenght} characters.");
        
        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price cannot be null.")
            .GreaterThan(productSettings.PriceMinLenght)
            .WithMessage($"The Price must be greater than {productSettings.PriceMinLenght}.");
        
        RuleFor(c => c.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .NotNull().WithMessage("Brand cannot be null.")
            .Length(productSettings.ProductBrandMinLength, productSettings.ProductBrandMaxLength)
            .WithMessage($"Brand must be between {productSettings.ProductBrandMinLength} and {productSettings.ProductBrandMaxLength} characters.");
        
        RuleFor(x => x.CategoryIds)
            .NotNull().WithMessage("Category IDs is required")
            .NotEmpty().WithMessage("At least one category is required.")
            .Must(c => c.Distinct().Count() == c.Count)
            .WithMessage("Category IDs must be unique.")
            .Must(o => o is { Count: >= 1 }).WithMessage("Category IDs must have at least one category ID.");  
        
        RuleFor(x => x.Images)
            .NotNull().WithMessage("Images is required")
            .Must(o => o is { Count: >= 1 }).WithMessage("Images must have at least one image.");  
        
        RuleFor(x => x.ProductVariants)
            .NotNull().WithMessage("Product Variants is required")
            .Must(o => o is { Count: >= 1 }).WithMessage("Product Variants must have at least one product variant.");  
    }
}