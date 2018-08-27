using AlsTradingPost.Domain.AuditDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;

namespace AlsTradingPost.Domain.AuditDomain.Mappings
{
    public class AuditDomainMappingProfile : Profile
    {
        public AuditDomainMappingProfile()
        {
            CreateMap<AuditEntityDdto, Domain.Models.Audit>();
            CreateMap<IAuditedEvent, AuditEntityDdto>();
        }
    }
}
