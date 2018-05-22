using System;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventHandlerResolver : IDomainEventHandlerResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventHandlerResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDomainEventHandler<IDomainEvent> Resolve(Type handlerType)
        {
             
                var thing =_serviceProvider.GetService(handlerType);
            return (IDomainEventHandler<IDomainEvent>) thing;
        }
    }
}
