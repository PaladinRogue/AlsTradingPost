using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;

namespace AlsTradingPost.Domain.AuditDomain.Handlers
{
    public class AuditedEventHandler : DomainEventHandler<IAuditedEvent>
    {
        private readonly IAuditCommandService _auditCommandService;

        public AuditedEventHandler(
            IDomainEventBus domainEventBus,
            IAuditCommandService auditCommandService) : base(domainEventBus)
        {
            _auditCommandService = auditCommandService;
        }

        public override void Handle(IAuditedEvent auditedEvent)
        {
             _auditCommandService.AuditEntity(Mapper.Map<IAuditedEvent, AuditEntityDdto>(auditedEvent));
        }
    }
}
