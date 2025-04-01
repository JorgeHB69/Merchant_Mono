using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.QueryCommands.Payment.Orders.Commands.Commands;
using merchant_api.Business.QueryCommands.Payment.Orders.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using merchant_api.Data.Models.Enums.Payment;
using merchant_api.Data.Repositories.Interfaces.Payment;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Payment;

[ApiController]
[Route("api/payment/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderDto request)
    {
        var result = await mediator.Send(new CreateOrderCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(page, pageSize));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<PaginatedResponseDto<OrderDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
    
    [HttpGet("{userId}/User/")]
    public async Task<ActionResult<List<OrderWithCompleteDetailsDto>>> GetAllOrdersBySpecificUser(
        Guid userId, 
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] OrderStatus? status,
        int page, 
        int pageSize)
    {
        var parameters = new OrderFilterParameters
        {
            StartDate = startDate,
            EndDate = endDate,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Status = status
        };
        
        var result = await mediator.Send(new GetOrdersBySpecificUserQuery(userId, parameters, page, pageSize));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<PaginatedResponseDto<OrderWithCompleteDetailsDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
}