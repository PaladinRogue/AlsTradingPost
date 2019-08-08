using Messaging.Common;

namespace Messaging.Setup.Infrastructure.Directors
{
    public interface IPendingMessageProvider : IProvider<IMessage>
    {
    }
}
