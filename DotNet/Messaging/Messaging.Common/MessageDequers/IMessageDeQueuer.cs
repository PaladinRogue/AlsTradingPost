using System.Collections.Generic;
using System.Threading.Tasks;
using Messaging.Common;

namespace Messaging.Setup.Infrastructure.DeQueuers
{
    public interface IMessageDeQueuer
    {
        Task DeQueueAsync(IMessage message,
            IEnumerable<MessageRegistration> messageRegistrations);
    }
}