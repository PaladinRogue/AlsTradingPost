using Common.Authentication.Enum;
using Common.Messaging.Interfaces;

namespace Message.Messages
{
    public class ApplicationCreatedMessage : IMessage
    {
        public string Name { get; set; }
        public AuthenticationProtocol AuthenticationProtocols { get; set; }
    }
}
