using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.Authentication.Mappings
{
    public class AuthenticationApplicationMappingProfile : Profile
    {
        public AuthenticationApplicationMappingProfile()
        {
            CreateMap<LoginAdto, LoginDdto>();
        }
    }
}
