using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Libray.Core.Domain.DomainEvents
{
    public class DomainEventSubscriberResolver : IDomainEventSubscriberResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventSubscriberResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IDomainEventSubscriber<T>> ResolveAll<T>() where T : IDomainEvent
        {
            List<IDomainEventSubscriber<T>> handlers = _serviceProvider.GetServices<IDomainEventSubscriber<T>>().ToList();
            foreach (Type @interface in typeof(T).GetInterfaces())
            {
                Type type = typeof(IDomainEventSubscriber<>);
                Type generic = type.MakeGenericType(@interface);
                handlers.AddRange(_serviceProvider.GetServices(generic).Cast<IDomainEventSubscriber<T>>());
            }

            return handlers;
        }
    }
}
