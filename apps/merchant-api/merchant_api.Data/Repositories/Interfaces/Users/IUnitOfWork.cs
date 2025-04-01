namespace merchant_api.Data.Repositories.Interfaces.Users;

public interface IUnitOfWork : IDisposable
{
    public IUserAddressRepository UserAddressRepository { get; }
    public IUserRepository UserRepository { get; }
    public Task<int> CommitAsync();
}

