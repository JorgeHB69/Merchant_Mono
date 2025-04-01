using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Data.Repositories.Interfaces.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithDetails(Guid id);
    Task<User?> GetUserByIdentityId(string identityId);
    Task<User?> GetUserByEmailAsync(string email);
}
