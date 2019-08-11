using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Messaging.Common.Serialisers
{
    public interface IMessageSerialiser
    {
        string Serialise(IMessage message);

        IMessage Deserialise(string serialisedMessage);
    }
}
