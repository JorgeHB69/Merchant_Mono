using System.Net.Http.Json;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using DotNetEnv;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;

namespace merchant_api.Business.Services.Payment.Clients;

public class ProductClientService
{
    private readonly HttpClient _httpClient;
    private readonly string _inventoryServiceRoute;

    public ProductClientService(HttpClient httpClient)
    {
        Env.Load("../../../../.env");
        _httpClient = httpClient;
        _inventoryServiceRoute = Env.GetString("INVENTORY_SERVICE_ROUTE") ?? 
                                 Environment.GetEnvironmentVariable("INVENTORY_SERVICE_ROUTE") ?? 
                                 throw new Exception("INVENTORY_SERVICE_ROUTE is not set");
    }
    
    public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
    {
        var url = $"{_inventoryServiceRoute}Product/{productId}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) 
            return null;
        
        var successResponse = await response.Content.ReadFromJsonAsync<SuccessResponse<ProductDto>>();
        return successResponse?.Data;
    }
    
    public async Task<ProductVariantDto?> GetProductVariantByIdAsync(Guid productVariantId)
    {
        var url = $"{_inventoryServiceRoute}ProductVariant/{productVariantId}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) 
            return null;
        
        var successResponse = await response.Content.ReadFromJsonAsync<SuccessResponse<ProductVariantDto>>();
        return successResponse?.Data;
    }
}

    