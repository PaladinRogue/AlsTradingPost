namespace Common.Messaging.Message.Interfaces
{
    public interface IMessageReciever
    {
        void Recieve(IMessage message, MessageSubscription messageSubscription);
    }
}