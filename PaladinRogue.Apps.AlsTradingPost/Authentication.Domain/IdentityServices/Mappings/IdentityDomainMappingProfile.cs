using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Authentication.Domain.IdentityServices.Mappings
{
    public class IdentityDomainMappingProfile : Profile
    {
        public IdentityDomainMappingProfile()
        {
            CreateMap<Identity, IdentityProjection>()
                .IncludeBase<IEntity, IVersionedProjection>();
            CreateMap<CreateIdentityDdto, Identity>();
            CreateMap<UpdateIdentityDdto, Identity>()
                .IncludeBase<IVersionedDdto, IEntity>();
        }
    }
}
