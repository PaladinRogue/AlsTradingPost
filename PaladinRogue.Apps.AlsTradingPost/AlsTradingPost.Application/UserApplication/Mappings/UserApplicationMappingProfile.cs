using AlsTradingPost.Application.UserApplication.Models;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.UserApplication.Mappings
{
    public class UserApplicationMappingProfile : Profile
    {
        public UserApplicationMappingProfile()
        {
            CreateMap<UserProjection, UserAdto>();
            CreateMap<FacebookUpdateAdto, CreateUserDdto>();
            CreateMap<FacebookUpdateAdto, UpdateUserDdto>();
            CreateMap<UserProjection, UpdateUserDdto>();
        }
    }
}
