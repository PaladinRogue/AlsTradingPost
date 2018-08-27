using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
using Common.Authentication.Domain.SessionDomain.Models;

namespace AlsTradingPost.Application.Authentication.Mappings
{
    public class AuthenticationApplicationMappingProfile : Profile
    {
        public AuthenticationApplicationMappingProfile()
        {
            CreateMap<LoginAdto, LoginDdto>();
            CreateMap<RefreshTokenAdto, RefreshSessionDdto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.SessionId))
                .ForMember(d => d.RefreshToken, opts => opts.MapFrom(s => s.Token));
        }
    }
}
