using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources;
using DomainEvent.Broker.Interfaces;

namespace DomainEvent.Broker
{
    public class InMemoryDomainEventBusSubscriptionsManager: IDomainEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<Subscription>> _handlers;
        private readonly List<Type> _domainEventTypes;

        public event EventHandler<string> OnDomainEventRemoved;

        public InMemoryDomainEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<Subscription>>();
            _domainEventTypes = new List<Type>();
        }
        
        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>(Action<T> handler)
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            var domainEventKey = _getDomainEventKey<T>();
            _doAddSubscription(typeof(TH), handler, domainEventKey);
            _domainEventTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            Subscription subscriptionToRemove = _findSubscriptionToRemove<T, TH>();

            var domainEventKey = _getDomainEventKey<T>();
            _doRemoveSubscription(domainEventKey, subscriptionToRemove);
        }

        public IEnumerable<Subscription> GetSubscribersForDomainEvent(Type domainEventType, bool includeInterfaces = false)
        {
            var subscriptions = new List<Subscription>();
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

        private IEnumerable<Subscription> _getSubscribersForDomainEvent(string domainEventName) => _handlers[domainEventName];

        private bool _hasSubscriptionsForDomainEvent(string domainEventName) => _handlers.ContainsKey(domainEventName);

        private string _getDomainEventKey<T>()
        {
            return typeof(T).Name;
        }

        private void _doAddSubscription(Type handlerType, Delegate handler, string domainEventName)
        {
            if (!_hasSubscriptionsForDomainEvent(domainEventName))
            {
                _handlers.Add(domainEventName, new List<Subscription>());
            }

            if (_handlers[domainEventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{domainEventName}'", nameof(handlerType));
            }

            _handlers[domainEventName].Add(Subscription.Create(handlerType, handler));
        }
        
        private void _doRemoveSubscription(string domainEventName, Subscription subscriptionToRemove)
        {
            if (subscriptionToRemove == null) return;

            _handlers[domainEventName].Remove(subscriptionToRemove);

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

        private Subscription _findSubscriptionToRemove<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>
        {
            var domainEventKey = _getDomainEventKey<T>();
            return _dDoFindSubscriptionToRemove(domainEventKey, typeof(TH));
        }

        private Subscription _dDoFindSubscriptionToRemove(string domainEventName, Type handlerType)
        {
            if (!_hasSubscriptionsForDomainEvent(domainEventName))
            {
                return null;
            }

            return _handlers[domainEventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
