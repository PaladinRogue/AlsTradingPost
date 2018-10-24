using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Messages
{
    public class RegisterApplicationMessage : IMessage
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
    }
}
