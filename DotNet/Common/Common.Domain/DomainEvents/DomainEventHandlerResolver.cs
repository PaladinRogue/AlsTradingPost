using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Domain.DomainEvents
{
    public class DomainEventHandlerResolver : IDomainEventHandlerResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventHandlerResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IDomainEventHandler<T>> ResolveAll<T>() where T : IDomainEvent
        {
            List<IDomainEventHandler<T>> handlers = _serviceProvider.GetServices<IDomainEventHandler<T>>().ToList();
            foreach (Type @interface in typeof(T).GetInterfaces())
            {
                Type type = typeof(IDomainEventHandler<>);
                Type generic = type.MakeGenericType(@interface);
                handlers.AddRange(_serviceProvider.GetServices(generic).Cast<IDomainEventHandler<T>>());
            }

            return handlers;
        }
    }
}
