using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Bases;

public abstract class BaseRegister : IRegister
{
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}