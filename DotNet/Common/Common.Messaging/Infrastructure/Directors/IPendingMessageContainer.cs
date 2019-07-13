using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Directors
{
    public interface IPendingMessageContainer : IContainer<IMessage>
    {
    }
}
