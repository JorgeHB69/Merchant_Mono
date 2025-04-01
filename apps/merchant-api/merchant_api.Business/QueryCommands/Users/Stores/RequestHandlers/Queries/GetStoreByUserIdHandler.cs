using AutoMapper;
using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Queries;

public class GetStoreByUserIdQueryHandler(
    IStoreRepository storeRepository,
    IResponseHandlingHelper responseHandlingHelper,
    IMapper mapper) : IRequestHandler<GetStoreByUserIdQuery, BaseResponse?>
{
    public async Task<BaseResponse?> Handle(GetStoreByUserIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Store> stores = await storeRepository.FindAsync(store => store.UserId == request.Id);
        if (!stores.Any())
            return responseHandlingHelper.NotFound<StoreDto>(
                "The user has not any stores associated with this request.");
        
        return responseHandlingHelper.Ok("The store with the follow id" + request.Id + " was found", mapper.Map<StoreDto>(stores.First()));
    }
}