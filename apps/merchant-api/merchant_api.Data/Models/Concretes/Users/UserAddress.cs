using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Users;

public class UserAddress : BaseEntity
{
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}
