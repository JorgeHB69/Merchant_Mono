using AutoMapper;
using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.QueryCommands.Users.Stores.Request.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Stores.RequestHandlers.Commands;

public class UpdateStoreCommandHandler(
    IStoreRepository storeRepository,
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<StoreDto> validator,
    IMapper mapper) : IRequestHandler<UpdateStoreCommand, BaseResponse>
{

    public async Task<BaseResponse> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
    {
        var validation = validator.Validate(request.StoreDto);
        if (!validation.IsValid) return responseHandlingHelper.BadRequest<StoreDto>(
            "The operation to create a store was not completed, please check the errors.",
            validation.Errors.Select(e => e.ErrorMessage).ToList());
        var store = mapper.Map<Store>(request.StoreDto);
        store = await storeRepository.UpdateAsync(store);
        return responseHandlingHelper.Ok("The category has been successfully updated.", mapper.Map<StoreDto>(store));
    }
}