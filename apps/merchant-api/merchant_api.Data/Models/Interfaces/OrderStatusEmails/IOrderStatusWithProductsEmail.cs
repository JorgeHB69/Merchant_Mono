namespace merchant_api.Data.Models.Interfaces.OrderStatusEmails
{
    public interface IOrderStatusWithProductsEmail : IOrderStatusEmail
    {
        public List<IProductItem> Products { get; set; }

    }
}