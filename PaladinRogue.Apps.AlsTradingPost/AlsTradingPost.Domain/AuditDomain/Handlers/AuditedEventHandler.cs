using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Setup.Infrastructure.Transactions;

namespace AlsTradingPost.Domain.AuditDomain.Handlers
{
    public class AuditedEventHandler : DomainEventHandler<IAuditedEvent, AuditedEventHandler>
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly IAuditCommandService _auditCommandService;

        public AuditedEventHandler(ITransactionFactory transactionFactory,
            IDomainEventBus domainEventBus, IAuditCommandService auditCommandService) : base(domainEventBus)
        {
            _transactionFactory = transactionFactory;
            _auditCommandService = auditCommandService;
        }

        public override void Handle(IAuditedEvent auditedEvent)
        {
            using (ITransaction transaction = _transactionFactory.Create())
            {
                _auditCommandService.AuditEntity(Mapper.Map<IAuditedEvent, AuditEntityDdto>(auditedEvent));

                transaction.Commit();
            }
        }
    }
}
