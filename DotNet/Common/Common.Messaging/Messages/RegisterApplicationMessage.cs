using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Messages
{
    public class RegisterApplicationMessage : IMessage
    {
        protected RegisterApplicationMessage()
        {
        }

        protected RegisterApplicationMessage(string name, string systemName, string hostUri, string adminEmailAddress)
        {
            Name = name;
            SystemName = systemName;
            HostUri = hostUri;
            AdminEmailAddress = adminEmailAddress;
        }

        public static RegisterApplicationMessage Create(string name, string systemName, string hostUri, string adminEmailAddress)
        {
            return new RegisterApplicationMessage(name, systemName, hostUri, adminEmailAddress);
        }

        public string Type => nameof(RegisterApplicationMessage);

        public string Name { get; set; }

        public string SystemName { get; set; }

        public string HostUri { get; set; }

        public string AdminEmailAddress { get; set; }
    }
}
