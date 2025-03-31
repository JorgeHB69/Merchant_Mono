using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Inventory;

public class WishList : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Product Product { get; set; }
}