using AutoMapper;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.SessionDomain.Models;

namespace Common.Authentication.Domain.SessionDomain.Mappings
{
    public class SessionDomainMappingProfile : Profile
    {
        public SessionDomainMappingProfile()
        {
            CreateMap<Session, SessionProjection>();
            CreateMap<Session, RefreshSessionProjection>();
        }
    }
}
