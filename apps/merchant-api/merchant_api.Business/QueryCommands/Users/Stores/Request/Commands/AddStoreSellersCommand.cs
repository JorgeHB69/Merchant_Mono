using MediatR;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Stores.Request.Commands;
public class AddStoreSellersCommand(
    AddStoreSellersDto addStoreSellersDto)
    : IRequest<BaseResponse>
{
    public AddStoreSellersDto AddStoreSellersDto { get; set; } = addStoreSellersDto;
}
