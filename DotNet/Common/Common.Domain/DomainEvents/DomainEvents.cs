﻿using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public static class DomainEvents
    {
        private static volatile IDomainEventDispatcher _domainEventDispatcher;

        private static IDomainEventDispatcher DomainEventDispatcher
        {
            get => _domainEventDispatcher;
            set => _domainEventDispatcher = value;
        }

        public static void SetDomainEventDispatcher(this IDomainEventDispatcher domainEventDispatcher)
        {
            if (DomainEventDispatcher == null)
            {
                DomainEventDispatcher = domainEventDispatcher;
            }
        }

        public static Task RaiseAsync<T>(T domainEvent) where T : IDomainEvent
        {
            if (DomainEventDispatcher == null)
            {
                throw new DomainEventDispatcherNotSetException();
            }

            return DomainEventDispatcher.DispatchEventAsync(domainEvent);
        }
    }
}