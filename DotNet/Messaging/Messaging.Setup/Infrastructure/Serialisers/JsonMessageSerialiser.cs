using Newtonsoft.Json;
using PaladinRogue.Libray.Messaging.Common.Messages;
using PaladinRogue.Libray.Messaging.Common.Serialisers;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Serialisers
{
    public class JsonMessageSerialiser : IMessageSerialiser
    {
        private readonly JsonSerializerSettings _settings;

        public JsonMessageSerialiser()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public string Serialise(IMessage message)
        {
            return JsonConvert.SerializeObject(message, _settings);
        }

        public IMessage Deserialise(string serialisedMessage)
        {
            return JsonConvert.DeserializeObject<IMessage>(serialisedMessage, _settings);
        }
    }
}
