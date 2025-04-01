using MediatR;
using merchant_api.Business.QueryCommands.Users.UserAddresses.Request.Commands;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.UserAddresses.RequestHandlers.Commands;

public class CreateUserAddressCommandHandler(IUserAddressRepository userAddressRepository) : IRequestHandler<CreateUserAddressCommand, UserAddress>
{
    public async Task<UserAddress> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
    {
        UserAddress orderItem = request.UserAddress;
        orderItem = await userAddressRepository.AddAsync(orderItem);
        return orderItem;
    }
}

