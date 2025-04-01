using MediatR;
using merchant_api.Business.Dtos.Users;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Auth.Request.Commands;

public class UpdateUserCommand (UpdateUserDto updateUserDto) : IRequest<BaseResponse>
{
    public UpdateUserDto UpdateUserDto { get; set; } = updateUserDto;
}