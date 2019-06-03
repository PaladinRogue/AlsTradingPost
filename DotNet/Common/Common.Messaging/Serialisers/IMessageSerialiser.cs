using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Serialisers
{
    public interface IMessageSerialiser
    {
        string Serialise(IMessage message);

        IMessage Deserialise(string serialisedMessage);
    }
}
