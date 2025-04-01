using AutoMapper;
using FluentValidation;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.Dtos.Inventory.Images;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Business.Dtos.Payment.Orders;
using merchant_api.Business.Dtos.Payment.PaymentTransactions;
using merchant_api.Business.Profiles;
using merchant_api.Business.Services;
using merchant_api.Business.Services.Payment;
using merchant_api.Business.Services.Payment.Clients;
using merchant_api.Business.Validators.Inventory.Categories;
using merchant_api.Business.Validators.Inventory.Images;
using merchant_api.Business.Validators.Inventory.Products;
using merchant_api.Business.Validators.Inventory.ProductVariants;
using merchant_api.Business.Validators.Inventory.Variants;
using merchant_api.Business.Validators.Payment.Orders;
using merchant_api.Commons.ResponseHandler.Handler.Concretes;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Models.Concretes.Payment;
using merchant_api.Data.Repositories.Concretes;
using merchant_api.Data.Repositories.Concretes.Inventory;
using merchant_api.Data.Repositories.Concretes.Payment;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;
using merchant_api.Data.Repositories.Interfaces.Payment;
using merchant_api.Data.Repositories.Utils;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Validators.PaymentTransactions;

namespace merchant_api.Business;

public static class ApplicationConfiguration
{
    public static void AddConfigurations(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateImageDto).Assembly)
        );
        
        var mapperConfig = new AutoMapper.MapperConfiguration(mc => {
            mc.AddMaps(typeof(ProductProfile).Assembly);
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        services.AddScoped<HttpClient>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<IRepository<Image>, ImageRepository>();
        services.AddScoped<IRepository<ProductAttribute>, ProductAttributeRepository>();
        services.AddScoped<IRepository<ProductVariant>, ProductVariantRepository>();
        services.AddScoped<IRepository<Variant>, VariantRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWishListRepository, WishListRepository>();
        
        services.AddTransient<ProductVariantService>();
        services.AddTransient<HttpStockThresholdService>();
        services.AddTransient<ProductService>();
        services.AddTransient<ProductFiltersManager>();
        services.AddTransient<StockThresholdNoticationService>();
        services.AddScoped<IResponseHandlingHelper, ResponseHandlingHelper>();
        
        services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
        services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();
        services.AddScoped<IValidator<CreateImageDto>, CreateImageValidator>();
        services.AddScoped<IValidator<UpdateImageDto>, UpdateImageValidator>();
        services.AddScoped<IValidator<CreateVariantDto>, CreateVariantValidator>();
        services.AddScoped<IValidator<UpdateVariantDto>, UpdateVariantValidator>();
        services.AddScoped<IValidator<CreateProductVariantDto>, CreateProductVariantValidator>();
        services.AddScoped<IValidator<UpdateProductVariantDto>, UpdateProductVariantValidator>();
        services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductValidator>();
        
        //Payment
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
        services.AddScoped<IRepository<PaymentMethod>, PaymentMethodRepository>();
        services.AddScoped<IRepository<PaymentTransaction>, PaymentTransactionRepository>();
        
        services.AddTransient<OrderItemService>();
        services.AddTransient<OrderService>();
        services.AddTransient<OrderItemsWithExtraDetailsService>();
        services.AddTransient<PaymentTransactionService>();
        
        services.AddScoped<IResponseHandlingHelper, ResponseHandlingHelper>();
        services.AddTransient<ProductClientService>();
        
        services.AddScoped<IValidator<CreateOrderDto>, CreateOrderValidator>();
        services.AddScoped<IValidator<CreatePaymentTransactionDto>, CreatePaymentTransactionValidator>();
    }
}