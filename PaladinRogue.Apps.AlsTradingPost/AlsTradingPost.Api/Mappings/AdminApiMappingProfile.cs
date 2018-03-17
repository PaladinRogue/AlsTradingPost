using AlsTradingPost.Api.Request.Admin;
using AlsTradingPost.Api.Resources.Admin;
using AlsTradingPost.Application.Admin.Models;
using AutoMapper;

namespace AlsTradingPost.Api.Mappings
{
    public class AdminApiMappingProfile : Profile
    {
        public AdminApiMappingProfile()
        {
            CreateMap<AdminAdto, AdminResource>();
            CreateMap<CreateAdminRequestDto, CreateAdminAdto>();
            CreateMap<UpdateAdminRequestDto, UpdateAdminAdto>();
        }
    }
}
