using AutoMapper;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Authentication.Domain.SessionDomain.Mappings
{
    public class SessionDomainMappingProfile : Profile
    {
        public SessionDomainMappingProfile()
        {
            CreateMap<Session, SessionProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateSessionDdto, Session>();
            CreateMap<UpdateSessionDdto, Session>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
