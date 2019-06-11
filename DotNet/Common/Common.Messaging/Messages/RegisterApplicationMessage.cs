using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Messages
{
    public class RegisterApplicationMessage : IMessage
    {
        protected RegisterApplicationMessage()
        {
            
        }
        
        protected RegisterApplicationMessage(string name, string systemName, string adminEmailAddress)
        {
            Name = name;
            SystemName = systemName;
            AdminEmailAddress = adminEmailAddress;
        }

        public static RegisterApplicationMessage Create(string name, string systemName, string adminEmailAddress)
        {
            return new RegisterApplicationMessage(name, systemName, adminEmailAddress);
        }

        public string Type => nameof(RegisterApplicationMessage);
        
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string AdminEmailAddress { get; set; }
    }
}
