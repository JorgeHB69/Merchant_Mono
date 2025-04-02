using MediatR;
using merchant_api.Business.Dtos.Payment;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Models.Enums.EnumsConverters;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Payment.PaymentTransactions.Queries.QueryHandlers;

public class GetAllPaymentTransactionsQueryHandler(
    IResponseHandlingHelper responseHandlingHelper,
    IRepository<PaymentTransaction> paymentTransactionRepository
    ) : IRequestHandler<GetAllPaymentTransactionsQuery, BaseResponse> 
{
    public async Task<BaseResponse> Handle(GetAllPaymentTransactionsQuery request, CancellationToken cancellationToken)
    {
        var totalPaymentTransactions = await paymentTransactionRepository.GetAllAsync(request.Page, request.PageSize);
        var converter = new PaymentStatusConverterService();
        List<PaymentTransactionDto> totalPaymentTransactionsDtos = [];
        
        foreach (var paymentTransaction in totalPaymentTransactions)
        {
            var paymentTransactionDto = new PaymentTransactionDto
            {
                PaymentTransactionId = paymentTransaction.Id,
                OrderId = paymentTransaction.OrderId,
                Amount = paymentTransaction.Amount,
                TransactionOrderStatus = converter.ConvertPaymentStatusToString(paymentTransaction.TransactionOrderStatus),
                PaymentMethod = new PaymentMethodDto
                {
                    PaymentMethodId = paymentTransaction.PaymentMethod.Id,
                    Name = paymentTransaction.PaymentMethod.Name
                }
            };
            totalPaymentTransactionsDtos.Add(paymentTransactionDto);
        }
        
        var paymentTransactionsToDisplay = new PaginatedResponsePaymentDto<PaymentTransactionDto>
        {
            Items = totalPaymentTransactionsDtos, 
            TotalCount = totalPaymentTransactionsDtos.Count, 
            Page = request.Page, 
            PageSize = request.PageSize
        };
        return responseHandlingHelper.Ok("Payment Transactions have been successfully obtained.", paymentTransactionsToDisplay);
    }
}