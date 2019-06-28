using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Senders
{
    public interface IMessageSender
    {
        void Send(IMessage message);
    }
}