using MediatR;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Commands;

public class DeleteStoreSellersCommandHandler(
    IStoreRepository storeRepository, 
    IUserRepository userRepository,
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<DeleteStoreSellersCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteStoreSellersCommand request, CancellationToken cancellationToken)
    {
        var store = await storeRepository.GetByIdAsync(request.StoreId);
        if (store == null)
            return responseHandlingHelper.BadRequest<Guid>("Store not found");
        
        var user = await userRepository.GetByIdAsync(request.SellerId);
        if (user == null)
            return responseHandlingHelper.BadRequest<Guid>($"User with ID {request.SellerId} not found");

        var response = store.SellerIds.Remove(request.SellerId);

        user.UserType = UserType.CLIENT;

        await userRepository.UpdateAsync(user);
        
        await storeRepository.UpdateAsync(store);

        return responseHandlingHelper.Ok("The seller has been deleted", response);
    }
}