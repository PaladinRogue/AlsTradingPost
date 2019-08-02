using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Serialisers
{
    public interface IMessageSerialiser
    {
        string Serialise(IMessage message);

        IMessage Deserialise(string serialisedMessage);
    }
}
