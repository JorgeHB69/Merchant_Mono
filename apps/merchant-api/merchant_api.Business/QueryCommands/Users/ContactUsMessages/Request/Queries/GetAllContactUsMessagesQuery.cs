using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Queries;

public class GetAllContactUsMessagesQuery : IRequest<BaseResponse>;