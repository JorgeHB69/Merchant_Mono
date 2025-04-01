using FluentValidation;
using MassTransit;
using MediatR;
using merchant_api.Business.Dtos.Users;
using merchant_api.Business.QueryCommands.Users.Auth.Request.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Emails;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Auth.RequestHandlers.Commands;

public class UserRegisterCommandHandler(
    IUserRepository userRepository,
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<RegisterUserDto> validator,
    IBus producer) :
    IRequestHandler<UserRegisterCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var registerUserDto = request.RegisterUserDto;
        var response = await validator.ValidateAsync(registerUserDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<RegisterUserDto>(
            "The operation to create a user was not completed, please check the errors.",
            response.Errors.Select(e => e.ErrorMessage).ToList());
        var usersFound = await userRepository.FindAsync(user => user.Email == registerUserDto.Email);
        if (usersFound.Any())
            return responseHandlingHelper.BadRequest<User>("User already exists.");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = registerUserDto.Email,
            Name = registerUserDto.Name,
            IdentityId = registerUserDto.IdentityId
        };

        if (registerUserDto.EmailVerified)
        {
            await producer.Publish(
             new WelcomeEmail(new Contact(registerUserDto.Name ?? "", registerUserDto.Email ?? "")),
             CancellationToken.None
         );
        }

        user = await userRepository.AddAsync(user);
        return responseHandlingHelper.Created("The user was added successfully.", user.Id);
    }
}
