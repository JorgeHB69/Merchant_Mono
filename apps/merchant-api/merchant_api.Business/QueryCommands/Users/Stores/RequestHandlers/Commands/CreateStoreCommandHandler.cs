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

public class CreateStoreCommandHandler(
    IStoreRepository storeRepository, 
    IUserRepository userRepository,
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<StoreDto> validator,
    IMapper mapper) : IRequestHandler<CreateStoreCommand, BaseResponse>
{

    public async Task<BaseResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var validation = validator.Validate(request.StoreDto);
        if (!validation.IsValid) return responseHandlingHelper.BadRequest<StoreDto>(
            "The operation to create a store was not completed, please check the errors.", 
            validation.Errors.Select(e => e.ErrorMessage).ToList());
        
        var store = mapper.Map<Store>(request.StoreDto);
        
        var user = await userRepository.GetByIdAsync(store.UserId);
        if (user is null)
            return responseHandlingHelper.BadRequest<StoreDto>("User not found");

        var storeExist = await storeRepository.FindAsync((x) => x.UserId == store.UserId);

        store = await storeRepository.AddAsync(store);
        user.UserType = UserType.OWNER;
        await userRepository.UpdateAsync(user);
        return responseHandlingHelper.Created("The store has been created", store.Id);
    }
}