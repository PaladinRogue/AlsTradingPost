using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Common.Serialisers
{
    public interface IMessageSerialiser
    {
        string Serialise(IMessage message);

        IMessage Deserialise(string serialisedMessage);
    }
}
