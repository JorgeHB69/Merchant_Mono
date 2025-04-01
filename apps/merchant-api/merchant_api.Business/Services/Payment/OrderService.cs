using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Interfaces.Payment;

namespace merchant_api.Business.Services.Payment;

public class OrderService(
    IResponseHandlingHelper responseHandlingHelper,
    OrderItemService orderItemService,
    IOrderRepository orderRepository
)
{
    public async Task<BaseResponse> CreateOrder(CreateOrderDto orderDto)
    {
        double totalPrice = 0;

        var orderToCreate = new Order
        {
            UserId = orderDto.UserId,
            OrderStatus = orderDto.OrderStatus,
        };
        
        await orderRepository.AddAsync(orderToCreate);

        foreach (var orderItemDto in orderDto.Items)
        {
            var response =  await orderItemService.CreateOrderItem(orderItemDto, orderToCreate.Id);
            if (response is ErrorResponse errorResponse)
                return errorResponse;

            var successResponse = (SuccessResponse<OrderItem>)response;
            totalPrice += successResponse.Data.TotalPrice;
        }
        
        orderToCreate.TotalPrice = totalPrice;
        await orderRepository.UpdateAsync(orderToCreate);

        return responseHandlingHelper.Created("Order has been successfully created.", orderToCreate.Id);
    }
}