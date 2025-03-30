using AutoMapper;
using merchant_api.Business.Dtos.Images;
using merchant_api.Business.Dtos.Products;
using merchant_api.Business.Dtos.ProductVariants;
using merchant_api.Data.Models.Concretes;

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