using AlsTradingPost.Application.UserApplication.Models;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;

namespace AlsTradingPost.Api.FacebookAuth
{
    public class FacebookAuthApiMappingProfile : Profile
    {
        public FacebookAuthApiMappingProfile()
        {
            CreateMap<FacebookUserData, FacebookUpdateAdto>()
                .ForMember(d => d.PictureUrl, opts => opts.MapFrom(s => s.PictureData.Picture.Url));
        }
    }
}
