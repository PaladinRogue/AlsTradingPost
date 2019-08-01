using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.DeQueuers
{
    public interface IMessageDeQueuer
    {
        Task DeQueueAsync(IMessage message,
            IEnumerable<MessageRegistration> messageRegistrations);
    }
}