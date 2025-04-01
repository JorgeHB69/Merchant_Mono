using MediatR;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Queries;

public class GetStoreStockThresholdQueryHandler(
    IStoreRepository storeRepository,
    IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<GetStoreStockThresholdQuery, BaseResponse>
{

    public async Task<BaseResponse> Handle(GetStoreStockThresholdQuery request, CancellationToken cancellationToken)
    {
        var store = await storeRepository.GetByIdAsync(request.StoreId);
        if (store == null)
            return responseHandlingHelper.NotFound<int>("Store not found");

        return responseHandlingHelper.Ok("The store has the threshold: ", data: store.LowStockThreshold);
    }
}