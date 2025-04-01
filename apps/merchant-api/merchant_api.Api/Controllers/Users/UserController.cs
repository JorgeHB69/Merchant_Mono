using MediatR;
using merchant_api.Business.QueryCommands.Users.Users.Request.Commands;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using merchant_api.Data.Models.Concretes.Orders;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Users;

[ApiController]
[Route("api/users/[controller]")]
[Tags("Users")]
public class UserController(IMediator mediator) : ControllerBase
{

    [HttpPost("lowStock")]
    public async Task<ActionResult> SendLowStockInfo([FromBody] Dictionary<Guid, List<OrderItem>> request)
    {
        var result = await mediator.Send(new NotifyLowStockUsersCommand() { LowStockEmails = request });
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<bool>)result;
        return StatusCode(successResponse.StatusCode, successResponse);
    }
}
