using AlsTradingPost.Application.User.Models;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;

namespace AlsTradingPost.Api.Mappings
{
    public class UserApiMappingProfile : Profile
    {
        public UserApiMappingProfile()
        {
            CreateMap<FacebookUserData, FacebookUpdateAdto>()
                .ForMember(d => d.PictureUrl, opts => opts.MapFrom(s => s.PictureData.Picture.Url));
        }
    }
}
