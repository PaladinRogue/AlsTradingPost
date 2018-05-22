using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;

namespace AlsTradingPost.Domain.AuditDomain.Handlers
{
    public class AuditedEventHandler : IDomainEventHandler<IAuditedEvent>
    {
        private readonly IAuditCommandService _auditCommandService;

        public AuditedEventHandler(
            IAuditCommandService auditCommandService)
        {
            _auditCommandService = auditCommandService;
        }

        public void Handle(IAuditedEvent auditedEvent)
        {
             _auditCommandService.AuditEntity(Mapper.Map<IAuditedEvent, AuditEntityDdto>(auditedEvent));
        }
    }
}
