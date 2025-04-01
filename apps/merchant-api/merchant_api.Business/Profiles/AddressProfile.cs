using AutoMapper;
using merchant_api.Business.Dtos.Users;
using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Business.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<UserAddress, UserAddressDto>()
              .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address));

        CreateMap<UserAddressDto, UserAddress>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Street));
    }
}
