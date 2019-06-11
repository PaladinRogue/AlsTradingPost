namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IMessageReciever
    {
        void Recieve(IMessage message, MessageSubscription messageSubscription);
    }
}