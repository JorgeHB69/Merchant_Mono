using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.StripeWebHookRegister.Commands.Commands;

public class CreateEventRegisterWebHookCommand(string requestBody) : IRequest<BaseResponse>
{
    public string RequestBody { get; set; } = requestBody;
}
