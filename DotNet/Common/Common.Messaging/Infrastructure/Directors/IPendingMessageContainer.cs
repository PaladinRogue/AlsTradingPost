using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure.Directors
{
    public interface IPendingMessageContainer : IContainer<IMessage>
    {
    }
}
