using AutoMapper;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Business.Profiles;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreDto>();
        CreateMap<StoreDto, Store>();
    }
}
