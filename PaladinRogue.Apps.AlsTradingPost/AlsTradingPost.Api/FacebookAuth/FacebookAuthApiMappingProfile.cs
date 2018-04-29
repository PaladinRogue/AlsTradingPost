﻿using AlsTradingPost.Application.Authentication.Models;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;

namespace AlsTradingPost.Api.FacebookAuth
{
    public class FacebookAuthApiMappingProfile : Profile
    {
        public FacebookAuthApiMappingProfile()
        {
            CreateMap<FacebookUserData, LoginAdto>()
                .ForMember(d => d.PictureUrl, opts => opts.MapFrom(s => s.PictureData.Picture.Url));
        }
    }
}
