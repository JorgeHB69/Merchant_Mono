using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Commands.Commands;
using merchant_api.Business.Services.Payment;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Commands.CommandHandlers;

public class CreatePaymentTransactionCommandHandler(
    IValidator<CreatePaymentTransactionDto> validator,
    IResponseHandlingHelper responseHandlingHelper,
    PaymentTransactionService paymentTransactionService
    ) : IRequestHandler<CreatePaymentTransactionCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(CreatePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        var paymentTransactionDto = request.PaymentTransaction;
        var response = await validator.ValidateAsync(paymentTransactionDto, cancellationToken);
        if (!response.IsValid) 
            return responseHandlingHelper.BadRequest<CreatePaymentTransactionDto>(
                "The operation to create a payment transaction was not completed. Please check the errors.",
                response.Errors.Select(e => e.ErrorMessage).ToList()
            );
        
        return await paymentTransactionService.CreatePaymentTransaction(paymentTransactionDto);
    }
}