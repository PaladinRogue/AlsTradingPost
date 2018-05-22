using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker.Interfaces;

namespace DomainEvent.Broker
{
    public class InMemoryDomainEventBusSubscriptionsManager: IDomainEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<DomainEventSubscription>> _handlers;
        private readonly List<Type> _domainEventTypes;

        public event EventHandler<string> OnDomainEventRemoved;

        public InMemoryDomainEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<DomainEventSubscription>>();
            _domainEventTypes = new List<Type>();
        }
        
        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            string domainEventKey = _getDomainEventKey<T>();
            _doAddSubscription(typeof(TH), domainEventKey);
            _domainEventTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            DomainEventSubscription domainEventSubscriptionToRemove = _findSubscriptionToRemove<T, TH>();

            string domainEventKey = _getDomainEventKey<T>();
            _doRemoveSubscription(domainEventKey, domainEventSubscriptionToRemove);
        }

        public IEnumerable<DomainEventSubscription> GetSubscribersForDomainEvent(Type domainEventType, bool includeInterfaces = false)
        {
            List<DomainEventSubscription> subscriptions = new List<DomainEventSubscription>();
            if (_hasSubscriptionsForDomainEvent(domainEventType.Name))
            {
                subscriptions.AddRange(_getSubscribersForDomainEvent(domainEventType.Name));
            }

            if (includeInterfaces)
            {
                foreach (Type @interface in domainEventType.GetInterfaces())
                {
                    if (_hasSubscriptionsForDomainEvent(@interface.Name))
                    {
                        subscriptions.AddRange(_getSubscribersForDomainEvent(@interface.Name));
                    }
                }
            }

            return subscriptions;
        }

        private IEnumerable<DomainEventSubscription> _getSubscribersForDomainEvent(string domainEventName) => _handlers[domainEventName];

        private bool _hasSubscriptionsForDomainEvent(string domainEventName) => _handlers.ContainsKey(domainEventName);

        private string _getDomainEventKey<T>()
        {
            return typeof(T).Name;
        }

        private void _doAddSubscription(Type handlerType, string domainEventName)
        {
            if (!_hasSubscriptionsForDomainEvent(domainEventName))
            {
                _handlers.Add(domainEventName, new List<DomainEventSubscription>());
            }

            if (_handlers[domainEventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{domainEventName}'", nameof(handlerType));
            }

            _handlers[domainEventName].Add(DomainEventSubscription.Create(handlerType));
        }
        
        private void _doRemoveSubscription(string domainEventName, DomainEventSubscription domainEventSubscriptionToRemove)
        {
            if (domainEventSubscriptionToRemove == null) return;

            _handlers[domainEventName].Remove(domainEventSubscriptionToRemove);

            if (_handlers[domainEventName].Any()) return;

            _handlers.Remove(domainEventName);
            Type domainEventType = _domainEventTypes.SingleOrDefault(e => e.Name == domainEventName);
            if (domainEventType != null)
            {
                _domainEventTypes.Remove(domainEventType);
            }

            _raiseOnDomainEventRemoved(domainEventName);
        }

        private void _raiseOnDomainEventRemoved(string domainEventName)
        {
            OnDomainEventRemoved?.Invoke(this, domainEventName);
        }

        private DomainEventSubscription _findSubscriptionToRemove<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            string domainEventKey = _getDomainEventKey<T>();
            return _dDoFindSubscriptionToRemove(domainEventKey, typeof(TH));
        }

        private DomainEventSubscription _dDoFindSubscriptionToRemove(string domainEventName, Type handlerType)
        {
            if (!_hasSubscriptionsForDomainEvent(domainEventName))
            {
                return null;
            }

            return _handlers[domainEventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
