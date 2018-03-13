using System;
using AlsTradingPost.Application.Admin.Models;
using AlsTradingPost.Domain.AdminServices.Models;
using AutoMapper;

namespace AlsTradingPost.Application.Admin.Mappings
{
    public class AdminApplicationMappingProfile : Profile
    {
        public AdminApplicationMappingProfile()
        {
            CreateMap<AdminProjection, AdminAdto>();
            CreateMap<AdminDdto, AdminAdto>();
            CreateMap<CreateAdminAdto, AdminDdto>()
                .ForMember(dest => dest.Id,
                    opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
