namespace merchant_api.Data.Models.Interfaces;

public interface IEntity : IRegister
{
    Guid Id { get; }
    bool IsActive { get; set; }
}