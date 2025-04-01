using MediatR;
using merchant_api.Business.Dtos.ContactUsMessages;
using merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.ContactUsMessages.RequestHandlers.Queries;

public class GetAllContactUsMessagesHandler(
    IResponseHandlingHelper responseHandlingHelper,
    IContactUsMessageRepository contactUsMessageRepository
    ) : IRequestHandler<GetAllContactUsMessagesQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllContactUsMessagesQuery request, CancellationToken cancellationToken)
    {
        var totalContactUsMessages = await contactUsMessageRepository.GetAllAsync();
        
        return responseHandlingHelper.Ok("Contact us messages have been successfully obtained.",
            totalContactUsMessages.Select(contactUsMessage => new ContactUsMessageDto
            {
                Id = contactUsMessage.Id,
                Name = contactUsMessage.Name,
                Email = contactUsMessage.Email,
                Message = contactUsMessage.Message
            }).ToList());
    }
}