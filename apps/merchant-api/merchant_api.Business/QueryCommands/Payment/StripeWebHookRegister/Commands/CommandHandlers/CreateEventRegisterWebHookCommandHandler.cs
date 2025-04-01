using DotNetEnv;
using MediatR;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Business.QueryCommands.Payment.StripeWebHookRegister.Commands.Commands;
using merchant_api.Business.Services.Payment;
using merchant_api.Business.Services.Payment.Clients;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;
using merchant_api.Data.Models.Enums.Payment;
using merchant_api.Data.Repositories.Interfaces;
using Stripe;
using Stripe.Checkout;
using PaymentMethod = merchant_api.Data.Models.Concretes.Payment.PaymentMethod;

namespace merchant_api.Business.QueryCommands.Payment.StripeWebHookRegister.Commands.CommandHandlers;

public class CreateEventRegisterWebHookCommandHandler : IRequestHandler<CreateEventRegisterWebHookCommand, BaseResponse>
{
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IResponseHandlingHelper _responseHandlingHelper;
    private readonly PaymentTransactionService _transactionService;
    private readonly OrderService _orderService;
    private readonly ProductClientService _productClientService;

    public CreateEventRegisterWebHookCommandHandler(
        IRepository<PaymentMethod> paymentMethodRepository,
        PaymentTransactionService paymentTransactionService,
        PaymentTransactionService transactionService, OrderService orderService, 
        ProductClientService productClientService,
        IResponseHandlingHelper responseHandlingHelper)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _transactionService = transactionService;
        _orderService = orderService;
        _productClientService = productClientService;
        _responseHandlingHelper = responseHandlingHelper;
        StripeConfiguration.ApiKey = Env.GetString("STRIPE_SECRET_KEY");
    }

    public async Task<BaseResponse> Handle(CreateEventRegisterWebHookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stripeEvent = EventUtility.ParseEvent(request.RequestBody);

            if (stripeEvent.Type != EventTypes.CheckoutSessionCompleted) 
                return _responseHandlingHelper.Ok("The stripe event was processed", stripeEvent.Id);
            
            if (stripeEvent.Data.Object is not Session session) 
                return _responseHandlingHelper.InternalServerError<Session>("Session was not found");

            var fullSession = await GetFullSession(session.Id, cancellationToken);
            
            var orderResponse = await ProcessOrder(fullSession);
            if (orderResponse is ErrorResponse errorOrderResponse) return errorOrderResponse;

            var transactionResponse = await ProcessPaymentTransaction(session, (SuccessResponse<Guid>)orderResponse);
            if (transactionResponse is ErrorResponse errorTransactionResponse) return errorTransactionResponse;
            
            //await PublishOrderEmailEvent(fullSession, (SuccessResponse<Guid>)orderResponse);
            
            return _responseHandlingHelper.Ok("The stripe event was processed", stripeEvent.Id);
        }
        catch (StripeException e)
        {
            return _responseHandlingHelper.InternalServerError<StripeException>(e.Message);
        }
    }
    
    private async Task<Session> GetFullSession(string sessionId, CancellationToken cancellationToken)
    {
        var sessionService = new SessionService();
        return await sessionService.GetAsync(sessionId, new SessionGetOptions
        {
            Expand = ["line_items", "line_items.data.price.product"]
        }, cancellationToken: cancellationToken);
    }
    
    private async Task<BaseResponse> ProcessOrder(Session session)
    {
        var orderDto = new CreateOrderDto
        {
            UserId = new Guid(session.Metadata["user_id"]),
            OrderStatus = OrderStatus.Pending,
            Items = CreateOrderItems(session)
        };

        return await _orderService.CreateOrder(orderDto);
    }

    private List<CreateOrderItemDto> CreateOrderItems(Session session)
    {
        var orderItems = new List<CreateOrderItemDto>();

        if (session.LineItems == null) return orderItems;

        foreach (var lineItem in session.LineItems.Data)
        {
            var productVariantId = lineItem.Price.Product.Metadata.GetValueOrDefault("product_variant_id");
            var currentOrderItem = new CreateOrderItemDto
            {
                ProductVariantId = new Guid(productVariantId!),
                Quantity = (int)lineItem.Quantity!,
                DiscountPercent = 0
            };
            orderItems.Add(currentOrderItem);
        }

        return orderItems;
    }

    private async Task<BaseResponse> ProcessPaymentTransaction(Session session, SuccessResponse<Guid> orderResponse)
    {
        var paymentMethod = await _paymentMethodRepository.AddAsync(new PaymentMethod
        {
            Name = session.PaymentMethodTypes[0]
        });
            
        var paymentTransactionDto = new CreatePaymentTransactionDto
        {
            PaymentMethodId = paymentMethod.Id,
            Amount = (double)session.AmountTotal! / 100,
            OrderId = orderResponse.Data
        };

        return await _transactionService.CreatePaymentTransaction(paymentTransactionDto);
    }
    
    /*private async Task PublishOrderEmailEvent(Session session, SuccessResponse<Guid> orderResponse)
    {
        var orderItems = new List<OrderItemWithPrice>();

        foreach (var lineItem in session.LineItems.Data)
        {
            var productVariantId = new Guid(lineItem.Price.Product.Metadata.GetValueOrDefault("product_variant_id")!);
            
            var variant = await _productClientService.GetProductVariantByIdAsync(productVariantId);
        
            if (variant == null)
            {
                continue;
            }

            var unitPrice = (decimal)lineItem.Price.UnitAmount! / 100m;
            var basePrice = unitPrice - (decimal)variant.PriceAdjustment;

            var orderItem = new OrderItemWithPrice(
                lineItem.Description ?? "Unknown Item", 
                (int)lineItem.Quantity!, 
                unitPrice,
                new ProductVariantDetails 
                (
                    productVariantId,
                    basePrice,
                    (decimal)variant.PriceAdjustment,
                    variant.Attributes.Select(a => new ProductVariantAttribute 
                        ( a.Name, a.Value)).ToList()
                )
            );

            orderItems.Add(orderItem);
        }

        var orderEmail = new OrderEmail(
            new Contact(
                session.CustomerDetails?.Name ?? "Customer", 
                session.CustomerDetails?.Email ?? string.Empty
            ), 
            new OrderNormal(
                orderResponse.Data.ToString(), 
                orderItems,
                (decimal)session.AmountTotal! / 100m
            )
        );

        await _producer.Publish(orderEmail);
    }*/
}
