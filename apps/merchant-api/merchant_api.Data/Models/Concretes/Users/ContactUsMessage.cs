using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Users;

public class ContactUsMessage : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}