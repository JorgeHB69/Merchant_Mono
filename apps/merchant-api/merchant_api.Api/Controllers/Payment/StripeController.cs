using MediatR;
using merchant_api.Business.Dtos.Payment.CheckoutSessions;
using merchant_api.Business.QueryCommands.Payment.StripeCheckoutSessions.Commands.CommandHandlers;
using merchant_api.Business.QueryCommands.Payment.StripeWebHookRegister.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Payment;

[ApiController]
[Route("api/payment/[controller]")]
public class StripeController(IMediator mediator) : ControllerBase
{
    [HttpPost("checkout-session/submit-cart")]
    public ActionResult<BaseResponse> InitCheckoutSession([FromBody] CheckoutSessionRequestDto request)
    {
        var result = mediator.Send(new CreateCheckoutSessionCommand(request)).Result;
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<string>)result;
        return successResponse;
    }
    
    [HttpPost("web-hook/register-web-hook-event")]
    public async Task<ActionResult<BaseResponse>> ManageStripeWebHookDetection()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var result = await mediator.Send(new CreateEventRegisterWebHookCommand(json));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<string>)result;
        return successResponse;
    }
}