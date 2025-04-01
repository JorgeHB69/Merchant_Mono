namespace merchant_api.Data.Models.Interfaces.OrderStatusEmails
{
    public interface IOrderStatusWithTimeEmail : IOrderStatusEmail
    {
        public DateTime Time { get; set; }
    }
}