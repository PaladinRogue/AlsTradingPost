using AlsTradingPost.Application.User.Models;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;

namespace AlsTradingPost.Api.Mappings
{
    public class UserApiMappingProfile : Profile
    {
        public UserApiMappingProfile()
        {
            CreateMap<FacebookUserData, UpdateUserAdto>()
                .ForMember(d => d.KnownAs, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.PictureUrl, opts => opts.MapFrom(s => s.PictureData.Picture.Url));
        }
    }
}
