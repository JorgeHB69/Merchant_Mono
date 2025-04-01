using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.Dtos.Payment.Products;
using merchant_api.Business.Services.Payment.Clients;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.Services.Payment;

public class OrderItemService(
    IResponseHandlingHelper responseHandlingHelper,
    ProductClientService productClientService,
    IRepository<OrderItem> orderItemRepository
    )
{
    public async Task<BaseResponse> CreateOrderItem(CreateOrderItemDto orderItemDto,  Guid orderId)
    {
        var productVariant = await productClientService.GetProductVariantByIdAsync(orderItemDto.ProductVariantId);
        if (productVariant == null)
            return responseHandlingHelper.NotFound<ProductVariantDto>(
                "Product variant with the follow id " + orderItemDto.ProductVariantId + " was not found");
            
        var product = await productClientService.GetProductByIdAsync(productVariant.ProductId);
        if (product == null)
            return responseHandlingHelper.NotFound<ProductDto>(
                "Product with the follow id " + orderItemDto.ProductVariantId + " was not found");

        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ProductVariantId = orderItemDto.ProductVariantId,
            Quantity = orderItemDto.Quantity,
            UnitPrice = product.Price + productVariant.PriceAdjustment,
            DiscountPercent = orderItemDto.DiscountPercent,
            TotalPrice = (product.Price + productVariant.PriceAdjustment) * orderItemDto.Quantity
        };
        
        await orderItemRepository.AddAsync(orderItem);
        return responseHandlingHelper.Created("Order Item has been successfully created.", orderItem);
    }
}