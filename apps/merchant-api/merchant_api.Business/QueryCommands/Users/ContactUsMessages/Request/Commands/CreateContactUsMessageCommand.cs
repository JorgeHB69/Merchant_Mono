using MediatR;
using merchant_api.Business.Dtos.ContactUsMessages;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Commands;

public class CreateContactUsMessageCommand(CreateContactUsMessageDto contactUsMessageDto) : IRequest<BaseResponse>
{
    public CreateContactUsMessageDto CreateContactUsMessageDto { get; set; } = contactUsMessageDto;
}