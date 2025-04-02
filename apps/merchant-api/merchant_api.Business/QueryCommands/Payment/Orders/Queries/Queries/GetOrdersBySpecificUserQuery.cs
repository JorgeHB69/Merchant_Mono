using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Payment;

namespace merchant_api.Business.QueryCommands.Payment.Orders.Queries.Queries;

public class GetOrdersBySpecificUserQuery(Guid userId, OrderFilterParameters parameters, int page, int pageSize) 
    : IRequest<BaseResponse>
{
    public Guid UserId { get; set; } = userId;
    public OrderFilterParameters FilterParameters { get; set; } = parameters;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}