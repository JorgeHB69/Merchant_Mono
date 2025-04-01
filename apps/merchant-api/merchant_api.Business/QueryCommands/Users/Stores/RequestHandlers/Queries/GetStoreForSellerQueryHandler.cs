using AutoMapper;
using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Queries;

public class GetStoreForSellerQueryHandler(
    IStoreRepository storeRepository, 
    IResponseHandlingHelper responseHandlingHelper,
    IMapper mapper)
    : IRequestHandler<GetStoreForSellerQuery, BaseResponse?>
{
    public async Task<BaseResponse?> Handle(GetStoreForSellerQuery request, CancellationToken cancellationToken)
    {
        var store = await storeRepository.GetStoreForSellerAsync(request.SellerId);
        if (store == null)
            return responseHandlingHelper.NotFound<StoreDto>("Any Store was found");
        
        return responseHandlingHelper.Ok("The store associate withe the seller was found", mapper.Map<StoreDto>(store));
    }
}
