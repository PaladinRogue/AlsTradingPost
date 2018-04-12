using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;

namespace AlsTradingPost.Application.User.Mappings
{
    public class UserApplicationMappingProfile : Profile
    {
        public UserApplicationMappingProfile()
        {
            CreateMap<UserProjection, UserAdto>();
            CreateMap<UserSummaryProjection, UserSummaryAdto>();
            CreateMap<FacebookUpdateAdto, CreateUserDdto>();
            CreateMap<FacebookUpdateAdto, UpdateUserDdto>();
            CreateMap<UserProjection, UpdateUserDdto>();
        }
    }
}
