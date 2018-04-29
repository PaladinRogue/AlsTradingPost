using AlsTradingPost.Application.Admin.Models;
using AlsTradingPost.Domain.AdminDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.Admin.Mappings
{
    public class AdminApplicationMappingProfile : Profile
    {
        public AdminApplicationMappingProfile()
        {
            CreateMap<AdminProjection, AdminAdto>();
            CreateMap<CreateAdminAdto, CreateAdminDdto>();
        }
    }
}
