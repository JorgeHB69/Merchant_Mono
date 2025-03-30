using AutoMapper;
using FluentValidation;
using merchant_api.Business.Dtos.Categories;
using merchant_api.Business.Dtos.Images;
using merchant_api.Business.Dtos.Products;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Business.Dtos.Variants;
using merchant_api.Business.Profiles;
using merchant_api.Business.Services;
using merchant_api.Business.Validators.Categories;
using merchant_api.Business.Validators.Images;
using merchant_api.Business.Validators.Products;
using merchant_api.Business.Validators.ProductVariants;
using merchant_api.Business.Validators.Variants;
using merchant_api.Commons.ResponseHandler.Handler.Concretes;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Concretes;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Utils;
using Microsoft.Extensions.DependencyInjection;

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
    }
}