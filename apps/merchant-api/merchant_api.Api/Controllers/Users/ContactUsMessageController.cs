using MediatR;
using merchant_api.Business.Dtos.ContactUsMessages;
using merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Commands;
using merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Queries;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Users;

[ApiController]
[Route("api/users/[controller]")]
[Tags("Users")]
public class ContactUsMessageController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateContactUsMessageDto request)
    {
        var result = await mediator.Send(new CreateContactUsMessageCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ContactUsMessageDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllContactUsMessagesQuery());
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<List<ContactUsMessageDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
}