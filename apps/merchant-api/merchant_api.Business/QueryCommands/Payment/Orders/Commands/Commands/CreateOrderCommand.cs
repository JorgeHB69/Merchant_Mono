using MediatR;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.Orders.Commands.Commands;

public class CreateOrderCommand(CreateOrderDto createOrderDto) : IRequest<BaseResponse>
{
    public CreateOrderDto OrderToCreateDto { get; set; } = createOrderDto;
}