using MassTransit;
using MediatR;
using merchant_api.Business.QueryCommands.Users.Users.Request.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Emails;
using merchant_api.Data.Models.Concretes.Orders;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.Users.RequestHandlers.Commands;


public class NotifyLowStockUsersCommandHandler(IUserRepository userRepository, IStoreRepository storeRepository, IResponseHandlingHelper responseHandlingHelper, IBus producer) : IRequestHandler<NotifyLowStockUsersCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(NotifyLowStockUsersCommand request, CancellationToken cancellationToken)
    {
        foreach (var lowStockDetail in request.LowStockEmails)
        {
            Guid storeID = lowStockDetail.Key;
            var store = await storeRepository.GetByIdAsync(storeID);
            if (store == null)
            {
                return responseHandlingHelper.BadRequest<bool>("The store with the follow id " + lowStockDetail.Key + " was not found", null);
            }

            List<Guid> sellerIds = [.. store.SellerIds];


            var users = await userRepository.FindAsync((user) => user.Store != null && user.Store.Id == storeID);

            if (users.Count() == 0)
            {
                return responseHandlingHelper.BadRequest<bool>("The users with the follow store id " + storeID + " were not found", null);
            }
            var usersList = users.ToList();

            foreach (var sellerId in sellerIds)
            {
                var seller = await userRepository.GetByIdAsync(sellerId);
                if (seller != null)
                {
                    usersList.Add(seller);
                }
            }

            foreach (var user in usersList)
            {
                await SendEmail(user.Name, user.Email, lowStockDetail.Value);
            }
        }

        return responseHandlingHelper.Ok("The users have been successfully notified.", true);
    }


    private async Task SendEmail(string? name, string? email, List<OrderItem> lowStockDetail)
    {
        await producer.Publish(new LowStockEmail(
                new Contact(
                    name ?? string.Empty,
                    email ?? string.Empty
                ),
                lowStockDetail,
                Environment.GetEnvironmentVariable("ADMIN_PANEL_URL") ?? string.Empty
                ));
    }
}

