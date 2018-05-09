using AlsTradingPost.Application.Authentication.Models;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;

namespace AlsTradingPost.Api.Authentication
{
    public class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<FacebookUserData, LoginAdto>()
                .ForMember(d => d.PictureUrl, opts => opts.MapFrom(s => s.PictureData.Picture.Url));
        }
    }
}
