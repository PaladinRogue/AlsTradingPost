using Messaging.Common;

namespace Messaging.Setup.Infrastructure.Directors
{
    public interface IPendingMessageContainer : IContainer<IMessage>
    {
    }
}
