using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;

namespace AlsTradingPost.Domain.AuditDomain.Handlers
{
    public class AuditedEventHandler : IDomainEventHandler<IAuditedEvent>
    {
        private readonly IAuditDomainService _auditDomainService;

        public AuditedEventHandler(IAuditDomainService auditDomainService)
        {
            _auditDomainService = auditDomainService;
        }

        public void Handle(IAuditedEvent auditedEvent)
        {
            _auditDomainService.AuditEntity(Mapper.Map<IAuditedEvent, AuditEntityDdto>(auditedEvent));
        }
    }
}
