using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Messages
{
    public class CreateSystemAdminIdentityMessage : IMessage
    {
        public string Identifier { get; set; }

        public string Password { get; set; }
    }
}
