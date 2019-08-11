using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.Messages;
using PaladinRogue.Libray.Messaging.Common.Registrations;

namespace PaladinRogue.Libray.Messaging.Common.MessageDequers
{
    public interface IMessageDeQueuer
    {
        Task DeQueueAsync(IMessage message,
            IEnumerable<MessageRegistration> messageRegistrations);
    }
}