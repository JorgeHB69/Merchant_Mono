using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.Auth.Request.Queries
{
    public class GetUserByTokenQuery(string token) : IRequest<BaseResponse?>
    {
        public string Token { get; set; } = token;
    }
}
