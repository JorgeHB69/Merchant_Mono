using merchant_api.Data.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Users;

public class UnitOfWork(DbContext context, IUserAddressRepository userAddressRepository, IUserRepository userRepository) : IUnitOfWork
{
    public IUserAddressRepository UserAddressRepository { get; } = userAddressRepository;

    public IUserRepository UserRepository { get; } = userRepository;

    private readonly DbContext _context = context;

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
