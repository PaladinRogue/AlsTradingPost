using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Handlers;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Common.Registrations;

namespace PaladinRogue.Library.Messaging.RabbitMQ.Registrations
{
    public class InMemoryMessageBusRegistrationsManager: IMessageBusRegistrationsManager
    {
        private readonly Dictionary<string, List<MessageRegistration>> _registrations;
        private readonly List<Type> _messageTypes;

        public event EventHandler<string> OnMessageRemoved;

        public InMemoryMessageBusRegistrationsManager()
        {
            _registrations = new Dictionary<string, List<MessageRegistration>>();
            _messageTypes = new List<Type>();
        }

        public bool IsEmpty => !_registrations.Keys.Any();

        public void Clear() => _registrations.Clear();

        public Task AddRegistrationAsync<T, TH>(Func<T, Task> asyncHandler)
            where T : IMessage
            where TH : IMessageHandler<T>
        {
            string messageKey = GetMessageKey<T>();
            _doAddRegistration(typeof(TH), asyncHandler, messageKey);
            _messageTypes.Add(typeof(T));

            return Task.CompletedTask;
        }

        public Task RemoveRegistrationAsync<T, TH>()
            where T : IMessage
            where TH : IMessageHandler<T>
        {
            MessageRegistration messageRegistrationToRemove = _findRegistrationToRemove<T, TH>();
            string messageKey = GetMessageKey<T>();
            _doRemoveRegistration(messageKey, messageRegistrationToRemove);

            return Task.CompletedTask;
        }

        public IEnumerable<MessageRegistration> GetRegistrationsForMessage<T>() where T : IMessage
        {
            string key = GetMessageKey<T>();
            return GetRegistrationsForMessage(key);
        }

        public IEnumerable<MessageRegistration> GetRegistrationsForMessage(string messageName) => _registrations[messageName];

        public bool HasRegistrationsForMessage<T>() where T : IMessage
        {
            string key = GetMessageKey<T>();
            return HasRegistrationsForMessage(key);
        }

        public bool HasRegistrationsForMessage(string messageName) => _registrations.ContainsKey(messageName);

        public string GetMessageKey<T>()
        {
            return typeof(T).Name;
        }

        private void _doAddRegistration<T>(Type handlerType, Func<T, Task> asyncHandler, string messageName)
        {
            if (!HasRegistrationsForMessage(messageName))
            {
                _registrations.Add(messageName, new List<MessageRegistration>());
            }

            if (_registrations[messageName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{messageName}'", nameof(handlerType));
            }

            _registrations[messageName].Add(MessageRegistration.Create(handlerType, asyncHandler));
        }

        private void _doRemoveRegistration(string messageName, MessageRegistration messageRegistrationToRemove)
        {
            if (messageRegistrationToRemove == null) return;

            _registrations[messageName].Remove(messageRegistrationToRemove);

            if (_registrations[messageName].Any()) return;

            _registrations.Remove(messageName);
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

        private MessageRegistration _findRegistrationToRemove<T, TH>()
            where T : IMessage
            where TH : IMessageHandler<T>
        {
            string messageKey = GetMessageKey<T>();
            return _doFindRegistrationToRemove(messageKey, typeof(TH));
        }

        private MessageRegistration _doFindRegistrationToRemove(string messageName, Type handlerType)
        {
            if (!HasRegistrationsForMessage(messageName))
            {
                return null;
            }

            return _registrations[messageName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}
