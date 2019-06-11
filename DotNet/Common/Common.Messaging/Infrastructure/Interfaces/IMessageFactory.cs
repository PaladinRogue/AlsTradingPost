namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IMessageFactory
    {
        IPreparedMessage Create(IMessage message);
    }
}
