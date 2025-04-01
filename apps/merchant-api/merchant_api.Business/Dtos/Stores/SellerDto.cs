using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Business.Dtos.Stores;

public class SellerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public UserType UserType { get; set; }
}
