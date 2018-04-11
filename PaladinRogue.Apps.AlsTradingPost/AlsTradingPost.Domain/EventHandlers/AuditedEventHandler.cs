using AlsTradingPost.Domain.Persistence;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Transactions;

namespace AlsTradingPost.Domain.EventHandlers
{
    public class AuditedEventHandler : DomainEventHandler<IAuditedEvent, AuditedEventHandler>
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly IAuditRepository _auditRepository;

        public AuditedEventHandler(ITransactionFactory transactionFactory, IAuditRepository auditRepository,
            IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            _transactionFactory = transactionFactory;
            _auditRepository = auditRepository;
        }

        public override void Handle(IAuditedEvent domainEvent)
        {
            using (ITransaction transaction = _transactionFactory.Create())
            {
                _auditRepository.AuditEntity(domainEvent.Entity);
                transaction.Commit();
            }
        }
    }
}
