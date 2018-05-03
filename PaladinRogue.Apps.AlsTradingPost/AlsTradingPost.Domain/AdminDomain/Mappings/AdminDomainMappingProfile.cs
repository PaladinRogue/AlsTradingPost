using AlsTradingPost.Domain.AdminDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AdminDomain.Mappings
{
    public class AdminDomainMappingProfile : Profile
    {
        public AdminDomainMappingProfile()
        {
            CreateMap<Admin, AdminProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateAdminDdto, Admin>();
            CreateMap<UpdateAdminDdto, Admin>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
