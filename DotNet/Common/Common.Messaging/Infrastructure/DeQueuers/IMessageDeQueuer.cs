using System.Collections.Generic;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.DeQueuers
{
    public interface IMessageDeQueuer
    {
        void DeQueue(IMessage message, IEnumerable<MessageSubscription> messageSubscriptions);
    }
}