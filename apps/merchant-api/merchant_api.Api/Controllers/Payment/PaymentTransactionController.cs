using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Commands.Commands;
using merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Payment;

[ApiController]
[Route("api/payment/[controller]")]
public class PaymentTransactionController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePaymentTransactionDto request)
    {
        var result = await mediator.Send(new CreatePaymentTransactionCommand(request));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);

        var successResponse = (SuccessResponse<Guid>)result;
        return StatusCode(successResponse.StatusCode, successResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PaymentTransactionDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllPaymentTransactionsQuery(page, pageSize));
        if (result is ErrorResponse errorResponse)
            return StatusCode(errorResponse.StatusCode, errorResponse);
        
        var successResponse = (SuccessResponse<PaginatedResponseDto<PaymentTransactionDto>>)result;
        return StatusCode(successResponse.StatusCode, successResponse);      
    }
}