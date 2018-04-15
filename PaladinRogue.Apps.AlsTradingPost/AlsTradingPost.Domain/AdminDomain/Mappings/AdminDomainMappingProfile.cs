using AlsTradingPost.Domain.AdminDomain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AdminDomain.Mappings
{
    public class AdminDomainMappingProfile : Profile
    {
        public AdminDomainMappingProfile()
        {
            CreateMap<Domain.Models.Admin, AdminProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateAdminDdto, Domain.Models.Admin>();
            CreateMap<UpdateAdminDdto, Domain.Models.Admin>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
