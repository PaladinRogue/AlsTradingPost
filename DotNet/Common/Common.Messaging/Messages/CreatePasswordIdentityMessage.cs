using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Messages
{
    public class CreatePasswordIdentityMessage : IMessage
    {
        public string Identifier { get; set; }
    }
}
