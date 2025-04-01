using FluentValidation;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.Services.Payment.Clients;

namespace merchant_api.Business.Validators.Payment.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator(ProductClientService productClientService)
    {
        RuleFor(order => order.UserId)
            .NotNull().WithMessage("User ID cannot be null.")
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(order => order.Items)
            .NotNull().WithMessage("Items cannot be null.")
            .NotEmpty().WithMessage("At least one item is required.") 
            .Must(items => items != null && items.All(item => item != null))
            .WithMessage("All items must be valid.");

        RuleForEach(order => order.Items)
            .SetValidator(new CreateOrderItemValidator(productClientService));
    }
}