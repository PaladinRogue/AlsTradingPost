using Authentication.Domain.ApplicationServices.Models;
using Authentication.Domain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Authentication.Domain.ApplicationServices.Mappings
{
    public class ApplicationDomainMappingProfile : Profile
    {
        public ApplicationDomainMappingProfile()
        {
            CreateMap<Application, ApplicationProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateApplicationDdto, Application>();
            CreateMap<UpdateApplicationDdto, Application>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
