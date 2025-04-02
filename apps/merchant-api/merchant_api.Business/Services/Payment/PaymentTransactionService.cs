using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Payment;

namespace merchant_api.Business.Services.Payment;

public class PaymentTransactionService(
    IResponseHandlingHelper responseHandlingHelper,
    IOrderRepository orderRepository,
    IRepository<PaymentMethod> paymentMethodRepository,
    IRepository<PaymentTransaction> paymentTransactionRepository
)
{
    public async Task<BaseResponse> CreatePaymentTransaction(CreatePaymentTransactionDto paymentTransactionDto)
    {
        var order = await orderRepository.GetByIdAsync(paymentTransactionDto.OrderId);
        if (order == null)
            return responseHandlingHelper.NotFound<Order>(
                $"Order with the id {paymentTransactionDto.OrderId} was not found.");

        var paymentMethod = await paymentMethodRepository.GetByIdAsync(paymentTransactionDto.PaymentMethodId);
        if (paymentMethod == null)
            return responseHandlingHelper.NotFound<PaymentMethod>(
                $"Payment Method with the id {paymentTransactionDto.PaymentMethodId} was not found.");

        var paymentTransaction = new PaymentTransaction
        {
            OrderId = paymentTransactionDto.OrderId,
            PaymentMethodId = paymentTransactionDto.PaymentMethodId,
            Amount = paymentTransactionDto.Amount,
            TransactionOrderStatus = paymentTransactionDto.TransactionOrderStatus,
        };

        await paymentTransactionRepository.AddAsync(paymentTransaction);

        return responseHandlingHelper.Created(
            "Payment Transaction has been successfully created.", paymentTransaction.Id);
    }
}