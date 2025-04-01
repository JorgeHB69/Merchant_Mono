using AutoMapper;
using merchant_api.Business.Dtos.Stores;
using merchant_api.Business.Dtos.Users;
using merchant_api.Data.Models.Concretes.Users;

namespace merchant_api.Business.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserType));
        CreateMap<UserDto, User>();
        CreateMap<User, SellerDto>();
    }
}
