using System;
using System.Collections.Generic;
using System.Linq;
using Common.Messaging.Message.Interfaces;
using Common.Resources;
using Message.Broker.Messages.Interfaces;

namespace Message.Broker.Messages
{
    public class InMemoryMessageBusSubscriptionsManager: IMessageBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<Subscription>> _handlers;
        private readonly List<Type> _messageTypes;

        public event EventHandler<string> OnMessageRemoved;

        public InMemoryMessageBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<Subscription>>();
            _messageTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            var messageKey = GetMessageKey<T>();
            _doAddSubscription(typeof(TH), handler, messageKey);
            _messageTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            Subscription subscriptionToRemove = _findSubscriptionToRemove<T, TH>();
            var messageKey = GetMessageKey<T>();
            _doRemoveSubscription(messageKey, subscriptionToRemove);
        }

        public IEnumerable<Subscription> GetSubscribersForMessage<T>() where T : IMessage
        {
            var key = GetMessageKey<T>();
            return GetSubscribersForMessage(key);
        }

        public IEnumerable<Subscription> GetSubscribersForMessage(string messageName) => _handlers[messageName];

        public bool HasSubscriptionsForMessage<T>() where T : IMessage
        {
            var key = GetMessageKey<T>();
            return HasSubscriptionsForMessage(key);
        }

        public bool HasSubscriptionsForMessage(string messageName) => _handlers.ContainsKey(messageName);

        public Type GetMessageTypeByName(string messageName) => _messageTypes.SingleOrDefault(t => t.Name == messageName);

        public string GetMessageKey<T>()
        {
            return typeof(T).Name;
        }

        private void _doAddSubscription(Type handlerType, Delegate handler, string messageName)
        {
            if (!HasSubscriptionsForMessage(messageName))
            {
                _handlers.Add(messageName, new List<Subscription>());
            }

            if (_handlers[messageName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{messageName}'", nameof(handlerType));
            }

            _handlers[messageName].Add(Subscription.Create(handlerType, handler));
        }
        
        private void _doRemoveSubscription(string messageName, Subscription subscriptionToRemove)
        {
            if (subscriptionToRemove == null) return;

            _handlers[messageName].Remove(subscriptionToRemove);

            if (_handlers[messageName].Any()) return;

            _handlers.Remove(messageName);
            Type messageType = _messageTypes.SingleOrDefault(e => e.Name == messageName);
            if (messageType != null)
            {
                _messageTypes.Remove(messageType);
            }

            _raiseOnMessageRemoved(messageName);
        }

        private void _raiseOnMessageRemoved(string messageName)
        {
            OnMessageRemoved?.Invoke(this, messageName);
        }

        private Subscription _findSubscriptionToRemove<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            var messageKey = GetMessageKey<T>();
            return _dDoFindSubscriptionToRemove(messageKey, typeof(TH));
        }

        private Subscription _dDoFindSubscriptionToRemove(string messageName, Type handlerType)
        {
            if (!HasSubscriptionsForMessage(messageName))
            {
                return null;
            }

            return _handlers[messageName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
