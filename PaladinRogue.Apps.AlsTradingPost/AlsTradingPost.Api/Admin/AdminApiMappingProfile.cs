using AlsTradingPost.Application.Admin.Models;
using AutoMapper;

namespace AlsTradingPost.Api.Admin
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
