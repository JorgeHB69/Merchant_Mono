using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.ContactUsMessages;
using merchant_api.Business.QueryCommands.Users.ContactUsMessages.Request.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Repositories.Interfaces.Users;

namespace merchant_api.Business.QueryCommands.Users.ContactUsMessages.RequestHandlers.Commands;

public class CreateContactUsMessageHandler(
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<CreateContactUsMessageDto> validator,
    IContactUsMessageRepository contactUsMessageRepository) : IRequestHandler<CreateContactUsMessageCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(CreateContactUsMessageCommand request, CancellationToken cancellationToken)
    {
        var contactUsMessageDto = request.CreateContactUsMessageDto;
        
        var response = await validator.ValidateAsync(contactUsMessageDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateContactUsMessageDto>(
            "The operation to create a contact us message was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());

        var contactUsMessage = new ContactUsMessage
        {
            Name = contactUsMessageDto.Name,
            Message = contactUsMessageDto.Message,
            Email = contactUsMessageDto.Email,
        };

        contactUsMessage = await contactUsMessageRepository.AddAsync(contactUsMessage);
        return responseHandlingHelper.Created("The contact us message was added successfully.", contactUsMessage.Id);;
    }
}