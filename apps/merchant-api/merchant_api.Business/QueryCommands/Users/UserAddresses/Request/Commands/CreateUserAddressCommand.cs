using MediatR;
using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Business.QueryCommands.Users.UserAddresses.Request.Commands;

public class CreateUserAddressCommand(UserAddress userAddress) : IRequest<UserAddress>
{
    public UserAddress UserAddress { get; set; } = userAddress;
}
