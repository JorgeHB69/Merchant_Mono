using MediatR;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Business.QueryCommands.Variants.Commands.Commands;
using merchant_api.Business.QueryCommands.Variants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers;

[ApiController]
[Route("api/inventory/[controller]")]
public class VariantController(IMediator mediator, IResponseHandlingHelper responseHandlingHelper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateVariantDto request)
    {
        var result = await mediator.Send(new CreateVariantCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<VariantDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetVariantByIdQuery(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<VariantDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
    
    [HttpGet]
    public async Task<ActionResult<List<VariantDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllVariantsQuery());
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<List<VariantDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);    
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVariantDto request)
    {
        if (id != request.Id) return StatusCode(400, responseHandlingHelper.BadRequest<Guid>(
            "The ID in the route and in the body of the request do not match."));
        
        var result = await mediator.Send(new UpdateVariantCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<VariantDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteVariantCommand(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<bool>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
}