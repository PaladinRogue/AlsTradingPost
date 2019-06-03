namespace Common.Messaging.Message.Interfaces
{
    public interface IMessageFactory
    {
        IPreparedMessage Create(IMessage message);
    }
}
