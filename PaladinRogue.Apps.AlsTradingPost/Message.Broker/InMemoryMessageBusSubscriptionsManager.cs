using System;
using System.Collections.Generic;
using System.Linq;
using Common.Messaging.Interfaces;
using Message.Broker.Interfaces;

namespace Message.Broker
{
    public class InMemoryMessageBusSubscriptionsManager : IMessageBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<Subscription>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryMessageBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<Subscription>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            var eventName = GetEventKey<T>();
            _doAddSubscription(typeof(TH), handler, eventName);
            _eventTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            Subscription handlerToRemove = _findSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            _doRemoveHandler(eventName, handlerToRemove);
        }

        public IEnumerable<Subscription> GetHandlersForEvent<T>() where T : IMessage
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<Subscription> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : IMessage
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        private void _doAddSubscription(Type handlerType, Delegate handler, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Subscription>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(Subscription.Create(handlerType, handler));
        }
        
        private void _doRemoveHandler(string eventName, Subscription subscriptionToRemove)
        {
            if (subscriptionToRemove == null) return;

            _handlers[eventName].Remove(subscriptionToRemove);

            if (_handlers[eventName].Any()) return;

            _handlers.Remove(eventName);
            Type eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
            if (eventType != null)
            {
                _eventTypes.Remove(eventType);
            }

            _raiseOnEventRemoved(eventName);
        }

        private void _raiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }

        private Subscription _findSubscriptionToRemove<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            var eventName = GetEventKey<T>();
            return _dDoFindSubscriptionToRemove(eventName, typeof(TH));
        }

        private Subscription _dDoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
