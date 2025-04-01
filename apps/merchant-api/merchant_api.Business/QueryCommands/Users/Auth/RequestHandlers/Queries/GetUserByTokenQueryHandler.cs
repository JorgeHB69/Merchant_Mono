using MediatR;
using merchant_api.Business.Dtos.Auth;
using merchant_api.Business.Dtos.Users;
using merchant_api.Business.QueryCommands.Users.Auth.Request.Queries;
using merchant_api.Business.Services.Auth.Interfaces;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Auth.RequestHandlers.Queries;

public class GetUserByTokenQueryHandler(
    IJwtDecoder jwtDecoder, 
    IUserRepository userRepository,
    IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<GetUserByTokenQuery, BaseResponse?>
{
    public async Task<BaseResponse?> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        request.Token = request.Token["Bearer ".Length..].Trim();

        AuthToken? authToken = await jwtDecoder.DecodeJwtToken(request.Token);

        if (authToken == null)
        {
            return responseHandlingHelper.NotFound<UserDto>(
                "The token could not be processed.");
        }

        var result = await userRepository.FindAsync(user => user.Email == authToken.Email);

        var user = result.FirstOrDefault();
        if (user == null)
        {
            return responseHandlingHelper.NotFound<UserDto>(
                "User not found.");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            UserType = user.UserType,
        };

        return responseHandlingHelper.Ok("The user with the follow id " + user.Id + " was found", userDto);
    }

}
