using AlsTradingPost.Application.AdminApplication.Models;
using AlsTradingPost.Domain.AdminDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.AdminApplication.Mappings
{
    public class AdminApplicationMappingProfile : Profile
    {
        public AdminApplicationMappingProfile()
        {
            CreateMap<AdminProjection, AdminAdto>();
            CreateMap<AdminSummaryProjection, AdminSummaryAdto>();
            CreateMap<CreateAdminAdto, CreateAdminDdto>();
        }
    }
}
