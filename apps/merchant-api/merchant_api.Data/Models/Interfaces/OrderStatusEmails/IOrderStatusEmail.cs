namespace merchant_api.Data.Models.Interfaces.OrderStatusEmails
{
    public interface IOrderStatusEmail : IEmail
    {
        public string OrderNumber { get; set; }
    }
}