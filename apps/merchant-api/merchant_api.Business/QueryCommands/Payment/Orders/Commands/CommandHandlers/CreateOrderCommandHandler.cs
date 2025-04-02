using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.QueryCommands.Payment.Orders.Commands.Commands;
using merchant_api.Business.Services.Payment;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.Orders.Commands.CommandHandlers;

public class CreateOrderCommandHandler(
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<CreateOrderDto> validator,
    OrderService orderService
    ) : IRequestHandler<CreateOrderCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderDto = request.OrderToCreateDto;
        var response = await validator.ValidateAsync(orderDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateOrderDto>(
            "The operation to create an order was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());

        return await orderService.CreateOrder(orderDto);
    }
}