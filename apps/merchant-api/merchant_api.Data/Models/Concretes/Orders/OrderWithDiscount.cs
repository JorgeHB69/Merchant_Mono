using merchant_api.Data.Models.Concretes.Orders.OrderItems;

namespace merchant_api.Data.Models.Concretes.Orders
{
    public class OrderWithDiscount : OrderNormal
    {
        public int DiscountPercentage { get; set; }
        public decimal OrderFinalTotal { get; set; }

        public OrderWithDiscount(string orderNumber, List<OrderItemWithPrice> orderItems, decimal orderTotal, int discountPercentage) : base(orderNumber, orderItems, orderTotal)
        {
            DiscountPercentage = discountPercentage;
            OrderFinalTotal = orderTotal;
        }
    }
}