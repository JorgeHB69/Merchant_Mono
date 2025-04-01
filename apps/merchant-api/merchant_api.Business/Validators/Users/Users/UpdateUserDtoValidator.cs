using FluentValidation;
using merchant_api.Business.Dtos.Users;
using merchant_api.Business.ValidatorSettings.Inventory;
using Microsoft.Extensions.Options;

namespace merchant_api.Business.Validators.Users.Users;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(IOptions<ValidationSettings> validationSettings)
    {
        var userSettings = validationSettings.Value.User;
        
        RuleFor(u => u.Name)
            .Length(userSettings.UserNameMinLength, userSettings.UserNameMaxLength)
            .WithMessage($"Name must be between {userSettings.UserNameMinLength} and {userSettings.UserNameMaxLength}.");
        RuleFor(u => u.Email)
            .Matches(userSettings.EmailRegex).WithMessage("Email must be a valid email address.");
    }
}