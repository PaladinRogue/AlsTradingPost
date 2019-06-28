using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Directors
{
    public interface IPendingMessageProvider : IProvider<IMessage>
    {
    }
}
