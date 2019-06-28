using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Factories
{
    public interface IMessageFactory
    {
        IPreparedMessage Create(IMessage message);
    }
}
