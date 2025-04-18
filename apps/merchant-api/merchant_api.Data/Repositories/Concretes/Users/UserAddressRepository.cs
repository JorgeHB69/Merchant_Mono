using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Users;

public class UserAddressRepository(DbContext context)
    : BaseRepository<UserAddress>(context), IUserAddressRepository
{}
