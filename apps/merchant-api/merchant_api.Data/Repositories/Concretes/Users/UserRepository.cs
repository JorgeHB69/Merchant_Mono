using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Users;

public class UserRepository(DbContext context) : BaseRepository<User>(context), IUserRepository
{
    public Task<User?> GetUserByIdentityId(string identityId)
    {
        return Context.Set<User>().FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }

    public async Task<User?> GetUserWithDetails(Guid id)
    {
        return await Context.Set<User>()
                            .Include(x => x.Address)
                            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public Task<User?> GetUserByEmailAsync(string email)
    {
        return Context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
    }
}
