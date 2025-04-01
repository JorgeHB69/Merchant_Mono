using merchant_api.Data.Models.Concretes.Orders.OrderItems;

namespace merchant_api.Data.Models.Concretes.Orders
{
    public class OrderNormal
    {
        public OrderNormal(string orderNumber, List<OrderItemWithPrice> orderItems, decimal orderTotal)
        {
            OrderNumber = orderNumber;
            OrderItems = orderItems;
            OrderTotal = orderTotal;
        }

        public string OrderNumber { get; set; }
        public List<OrderItemWithPrice> OrderItems { get; set; }
        public decimal OrderTotal { get; set; }
    }
}