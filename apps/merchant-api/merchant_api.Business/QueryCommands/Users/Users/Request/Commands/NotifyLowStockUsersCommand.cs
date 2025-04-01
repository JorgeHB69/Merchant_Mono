using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Orders;

namespace merchant_api.Business.QueryCommands.Users.Users.Request.Commands;

public class NotifyLowStockUsersCommand : IRequest<BaseResponse>
{
    public Dictionary<Guid, List<OrderItem>> LowStockEmails { get; set; } = [];
}