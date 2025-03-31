using AutoMapper;
using merchant_api.Business.Dtos.Inventory.Images;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Business.Dtos.Inventory.ProductVariants;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;

namespace merchant_api.Business.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductDto, UpdateProductDto>().ReverseMap();
        CreateMap<Image, UpdateImageDto>().ReverseMap();
        CreateMap<Image, ProductVariantImageDto>().ReverseMap();
        CreateMap<ProductVariant, UpdateProductVariantDto>().ReverseMap();
    }
}