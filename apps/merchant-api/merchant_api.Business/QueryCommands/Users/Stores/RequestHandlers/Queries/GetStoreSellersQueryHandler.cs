using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Queries;

public class GetStoreSellersQueryHandler(
    IStoreRepository storeRepository,
    IUserRepository userRepository,
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetStoreSellersQuery, List<BaseResponse>>
{
    public async Task<List<BaseResponse>> Handle(GetStoreSellersQuery request, CancellationToken cancellationToken)
    {
        var store = await storeRepository.GetByIdAsync(request.StoreId);
        if (store == null)
            return [responseHandlingHelper.NotFound<SellerDto>("Store not found")];
        
        var sellers = new List<SellerDto>();
        
        foreach (var sellerId in store.SellerIds)
        {
            var user = await userRepository.GetByIdAsync(sellerId);
            if (user != null)
            {
                sellers.Add(new SellerDto 
                { 
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserType = user.UserType
                });
            }
        }
        return [responseHandlingHelper.Ok("The sellers for this store are: ", sellers)];
    }
}
