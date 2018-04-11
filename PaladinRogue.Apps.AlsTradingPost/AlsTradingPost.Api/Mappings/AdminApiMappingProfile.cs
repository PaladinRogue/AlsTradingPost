using AlsTradingPost.Api.Resources.Admin;
using AlsTradingPost.Api.Templates.Admin;
using AlsTradingPost.Application.Admin.Models;
using AutoMapper;

namespace AlsTradingPost.Api.Mappings
{
    public class AdminApiMappingProfile : Profile
    {
        public AdminApiMappingProfile()
        {
            CreateMap<AdminAdto, AdminResource>();
            CreateMap<CreateAdminTemplate, CreateAdminAdto>();
            CreateMap<UpdateAdminTemplate, UpdateAdminAdto>();
        }
    }
}
