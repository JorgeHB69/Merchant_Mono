using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.QueryCommands.Categories.Commands.Commands;
using merchant_api.Business.QueryCommands.Categories.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers;

[ApiController]
[Route("api/inventory/[controller]")]
public class CategoryController(IMediator mediator, IResponseHandlingHelper responseHandlingHelper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var result = await mediator.Send(new CreateCategoryCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<CategoryDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);    
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllCategoriesQuery());
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<List<CategoryDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
    
    [HttpGet("{id}/Products")]
    public async Task<ActionResult<List<ProductDto>>> GetAllProductsBySpecificCategory(Guid id, Guid? userId, int page = 1, int pageSize = 10)
    {
        var result = await mediator.Send(new GetProductsBySpecificCategoryQuery(id, userId, page, pageSize));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<PaginatedResponseDto<ProductDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto request)
    {
        if (id != request.Id) return StatusCode(400, responseHandlingHelper.BadRequest<Guid>(
            "The ID in the route and in the body of the request do not match."));
        
        var result = await mediator.Send(new UpdateCategoryCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<CategoryDto>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<bool>)result;
        return StatusCode(successResponse.StatusCode, successResponse);   
    }
}