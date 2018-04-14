﻿using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AdminServices.Mappings
{
    public class AdminDomainMappingProfile : Profile
    {
        public AdminDomainMappingProfile()
        {
            CreateMap<Admin, AdminProjection>()
                .IncludeBase<IEntity, IVersionedProjection>();
            CreateMap<CreateAdminDdto, Admin>();
            CreateMap<UpdateAdminDdto, Admin>()
                .IncludeBase<IVersionedDdto, IEntity>();
        }
    }
}
