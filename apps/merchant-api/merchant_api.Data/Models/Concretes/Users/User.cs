using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Users;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? IdentityId { get; set; }
    public UserType UserType { get; set; } = UserType.CLIENT;
    public UserAddress? Address { get; set; }
    public Store? Store { get; set; }
}
