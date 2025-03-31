namespace merchant_api.Data.Models.Interfaces;

public interface IRegister
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
}
