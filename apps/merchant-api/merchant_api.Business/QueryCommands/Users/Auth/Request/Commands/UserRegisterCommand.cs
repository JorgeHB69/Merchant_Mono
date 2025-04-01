using MediatR;
using merchant_api.Business.Dtos.Users;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Auth.Request.Commands;

public class UserRegisterCommand(RegisterUserDto registerUserDto) : IRequest<BaseResponse>
{
    public RegisterUserDto RegisterUserDto { get; set; } = registerUserDto;
}
