using AlsTradingPost.Domain.AuditServices.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;

namespace AlsTradingPost.Domain.AuditServices.Mappings
{
    public class AuditDomainMappingProfile : Profile
    {
        public AuditDomainMappingProfile()
        {
            CreateMap<AuditEntityDdto, Audit>();
            CreateMap<IAuditedEvent, AuditEntityDdto>();
        }
    }
}
