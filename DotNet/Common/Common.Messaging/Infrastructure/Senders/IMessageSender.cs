using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure.Senders
{
    public interface IMessageSender
    {
        void Send(IMessage message);
    }
}