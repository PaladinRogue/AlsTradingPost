using System;
using System.Collections.Generic;
using System.Linq;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Subscribers;

namespace Messaging.Broker.Subscriptions
{
    public class InMemoryMessageBusSubscriptionsManager: IMessageBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<MessageSubscription>> _handlers;
        private readonly List<Type> _messageTypes;

        public event EventHandler<string> OnMessageRemoved;

        public InMemoryMessageBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<MessageSubscription>>();
            _messageTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            string messageKey = GetMessageKey<T>();
            _doAddSubscription(typeof(TH), handler, messageKey);
            _messageTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            MessageSubscription messageSubscriptionToRemove = _findSubscriptionToRemove<T, TH>();
            string messageKey = GetMessageKey<T>();
            _doRemoveSubscription(messageKey, messageSubscriptionToRemove);
        }

        public IEnumerable<MessageSubscription> GetSubscribersForMessage<T>() where T : IMessage
        {
            string key = GetMessageKey<T>();
            return GetSubscribersForMessage(key);
        }

        public IEnumerable<MessageSubscription> GetSubscribersForMessage(string messageName) => _handlers[messageName];

        public bool HasSubscriptionsForMessage<T>() where T : IMessage
        {
            string key = GetMessageKey<T>();
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
                _handlers.Add(messageName, new List<MessageSubscription>());
            }

            if (_handlers[messageName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{messageName}'", nameof(handlerType));
            }

            _handlers[messageName].Add(MessageSubscription.Create(handlerType, handler));
        }
        
        private void _doRemoveSubscription(string messageName, MessageSubscription messageSubscriptionToRemove)
        {
            if (messageSubscriptionToRemove == null) return;

            _handlers[messageName].Remove(messageSubscriptionToRemove);

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

        private MessageSubscription _findSubscriptionToRemove<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>
        {
            string messageKey = GetMessageKey<T>();
            return _dDoFindSubscriptionToRemove(messageKey, typeof(TH));
        }

        private MessageSubscription _dDoFindSubscriptionToRemove(string messageName, Type handlerType)
        {
            if (!HasSubscriptionsForMessage(messageName))
            {
                return null;
            }

            return _handlers[messageName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
