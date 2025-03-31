using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Inventory.Images;
using merchant_api.Business.QueryCommands.Images.Commands.Commands;
using merchant_api.Business.QueryCommands.Images.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers;

[ApiController]
[Route("api/inventory/[controller]")]
public class ImageController(IMediator mediator, IResponseHandlingHelper responseHandlingHelper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateImageDto request)
    {
        var result = await mediator.Send(new CreateImageCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ImageDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetImageByIdQuery(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<ImageDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ImageDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllImagesQuery(page, pageSize));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<PaginatedResponseDto<ImageDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);          
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateImageDto request)
    {
        if (id != request.Id) return StatusCode(400, responseHandlingHelper.BadRequest<Guid>(
            "The ID in the route and in the body of the request do not match."));

        var result = await mediator.Send(new UpdateImageCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<ImageDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteImageCommand(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<bool>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
}