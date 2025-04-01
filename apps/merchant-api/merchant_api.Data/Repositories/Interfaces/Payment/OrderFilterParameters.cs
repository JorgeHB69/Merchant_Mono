using merchant_api.Data.Models.Enums.Payment;

namespace merchant_api.Data.Repositories.Interfaces.Payment;

public class OrderFilterParameters
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public OrderStatus? Status { get; set; }
}