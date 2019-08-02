using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.Messages;

namespace Messaging.Broker.Registrations
{
    public interface IMessageBusRegistrationsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnMessageRemoved;

        Task AddRegistrationAsync<T, TH>(Func<T, Task> asyncHandler)
            where T : IMessage
            where TH : IMessageHandler<T>;

        Task RemoveRegistrationAsync<T, TH>()
            where T : IMessage
            where TH : IMessageHandler<T>;

        bool HasRegistrationsForMessage<T>() where T : IMessage;

        bool HasRegistrationsForMessage(string messageName);

        void Clear();

        IEnumerable<MessageRegistration> GetRegistrationsForMessage<T>() where T : IMessage;

        IEnumerable<MessageRegistration> GetRegistrationsForMessage(string messageName);

        string GetMessageKey<T>();
    }
}
