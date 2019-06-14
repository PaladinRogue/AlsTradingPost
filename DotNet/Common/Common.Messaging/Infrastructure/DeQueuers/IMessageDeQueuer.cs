using System.Collections.Generic;
using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure.DeQueuers
{
    public interface IMessageDeQueuer
    {
        void DeQueue(IMessage message, IEnumerable<MessageSubscription> messageSubscriptions);
    }
}