using Common.Messaging.Message.Interfaces;
using Common.Resources.Authentication;

namespace Common.Messaging.Messages
{
    public class ApplicationCreatedMessage : IMessage
    {
        public string Name { get; set; }
        public AuthenticationProtocol AuthenticationProtocols { get; set; }
    }
}
