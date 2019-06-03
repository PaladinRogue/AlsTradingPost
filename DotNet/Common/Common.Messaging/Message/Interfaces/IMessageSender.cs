namespace Common.Messaging.Message.Interfaces
{
    public interface IMessageSender
    {
        void Send(IMessage message);
    }
}