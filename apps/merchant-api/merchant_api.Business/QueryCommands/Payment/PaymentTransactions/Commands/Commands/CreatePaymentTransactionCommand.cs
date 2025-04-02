using MediatR;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Commands.Commands;

public class CreatePaymentTransactionCommand(CreatePaymentTransactionDto paymentTransactionDto) : IRequest<BaseResponse>
{
    public CreatePaymentTransactionDto PaymentTransaction { get; set; } = paymentTransactionDto;
}