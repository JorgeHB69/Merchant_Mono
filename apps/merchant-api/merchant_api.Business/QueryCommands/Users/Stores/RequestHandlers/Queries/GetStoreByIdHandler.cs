using AutoMapper;
using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Queries;

public class GetStoreByIdQueryHandler(
    IStoreRepository storeRepository, IMapper mapper,
    IResponseHandlingHelper responseHandlingHelper
    ) : IRequestHandler<GetStoreByIdQuery, BaseResponse?>
{
    public async Task<BaseResponse?> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        Store? store = await storeRepository.GetByIdAsync(request.Id);
        if (store == null)
            return responseHandlingHelper.NotFound<StoreDto>(
                "The store with the follow id" + request.Id + " was not found");
        return responseHandlingHelper.Ok("The store with the follow id" + request.Id + " was found", mapper.Map<StoreDto>(store));
    }
}