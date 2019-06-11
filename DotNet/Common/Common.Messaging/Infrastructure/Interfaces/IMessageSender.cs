namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IMessageSender
    {
        void Send(IMessage message);
    }
}