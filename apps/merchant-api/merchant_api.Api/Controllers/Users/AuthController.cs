using MediatR;
using merchant_api.Business.Dtos.Users;
using merchant_api.Business.QueryCommands.Users.Auth.Request.Commands;
using merchant_api.Business.QueryCommands.Users.Auth.Request.Queries;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Users;

[ApiController]
[Route("api/users/[controller]")]
[Tags("Users")]
public class AuthController(IMediator mediator) : ControllerBase
{

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp([FromBody] RegisterUserDto request)
    {
        var user = await mediator.Send(new UserRegisterCommand(request));
        if (user is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<Guid>)user;
        return StatusCode(successResponse.StatusCode, successResponse);
    }

    [HttpGet("token")]
    public async Task<ActionResult<UserDto?>> ValidateToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return Unauthorized();
        
        var user = await mediator.Send(new GetUserByTokenQuery(authHeader));
        if (user is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<UserDto>)user!;
        return StatusCode(successResponse.StatusCode, successResponse);
    }
    
    [HttpPut]
    public async Task<ActionResult<UserDto?>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return Unauthorized();

        var userResponse = await mediator.Send(new GetUserByTokenQuery(authHeader));
        if (userResponse is ErrorResponse userError)
            return StatusCode(userError.StatusCode, userError);
     
        var updateResponse = await mediator.Send(new UpdateUserCommand(updateUserDto));
        if (updateResponse is ErrorResponse updateError)
            return StatusCode(updateError.StatusCode, updateError);

        var successResponse = (SuccessResponse<UserDto>)updateResponse;
        return StatusCode(successResponse.StatusCode, successResponse);
    }
}
