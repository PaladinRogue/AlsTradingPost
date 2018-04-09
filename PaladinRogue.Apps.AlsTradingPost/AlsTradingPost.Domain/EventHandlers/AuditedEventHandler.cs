using AlsTradingPost.Domain.Persistence;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Transactions;

namespace AlsTradingPost.Domain.EventHandlers
{
    public class AuditedEventHandler : IDomainEventHandler<IAuditedEvent>, IDomainEventHandler
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly IAuditRepository _auditRepository;

        public AuditedEventHandler(ITransactionFactory transactionFactory, IAuditRepository auditRepository)
        {
            _transactionFactory = transactionFactory;
            _auditRepository = auditRepository;
        }
        public void Handle(IAuditedEvent domainEvent)
        {
            using (var transaction = _transactionFactory.Create())
            {
                _auditRepository.AuditEntity(domainEvent.Entity);
                transaction.Commit();
            }
        }

        public void Register()
        {
            DomainEventHandlerFactory.Register<IAuditedEvent>(Handle);
        }
    }
}
