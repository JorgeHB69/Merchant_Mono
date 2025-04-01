using MediatR;
using merchant_api.Business.Dtos.Payment;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.QueryCommands.Payment.Orders.Queries.Queries;
using merchant_api.Business.Services.Payment.Clients;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Enums.EnumsConverters;
using merchant_api.Data.Repositories.Interfaces.Payment;

namespace merchant_api.Business.QueryCommands.Payment.Orders.Queries.QueryHandlers;

public class GetAllOrdersQueryHandler(
    ProductClientService productClientService,
    IResponseHandlingHelper responseHandlingHelper,
    IOrderRepository orderRepository
    ) : IRequestHandler<GetAllOrdersQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var totalOrders = await orderRepository.GetAllAsync(request.Page, request.PageSize);
        var converter = new OrderStatusConverterService();
        List<OrderDto> totalOrdersDto = [];
        
        foreach (var order in totalOrders)
        {
            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                UserId = order.UserId,
                OrderStatus = converter.ConvertOrderStatusToString(order.OrderStatus),
                TotalPrice = order.TotalPrice,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.Id,
                    ProductId = productClientService.GetProductVariantByIdAsync(oi.ProductVariantId).Result!.ProductId,
                    ProductVariantId = oi.ProductVariantId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    DiscountPercent = oi.DiscountPercent,
                    SubTotalPrice = oi.TotalPrice
                }).ToList()
            };
            totalOrdersDto.Add(orderDto);
        }
        
        var paymentTransactionsToDisplay = new PaginatedResponsePaymentDto<OrderDto>
        {
            Items = totalOrdersDto, 
            TotalCount = totalOrdersDto.Count, 
            Page = request.Page, 
            PageSize = request.PageSize
        };
        return responseHandlingHelper.Ok("Orders have been successfully obtained.", paymentTransactionsToDisplay);
    }
}