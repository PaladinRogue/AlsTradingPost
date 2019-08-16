using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Common.Registrations;

namespace PaladinRogue.Library.Messaging.Common.MessageDequers
{
    public interface IMessageDeQueuer
    {
        Task DeQueueAsync(IMessage message,
            IEnumerable<MessageRegistration> messageRegistrations);
    }
}