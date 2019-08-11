using PaladinRogue.Libray.Messaging.Common.Messages;

namespace Gateway.Messages
{
    public class RegisterApplicationMessage : IMessage
    {
        protected RegisterApplicationMessage()
        {
        }

        protected RegisterApplicationMessage(string name, string systemName, string hostUri)
        {
            Name = name;
            SystemName = systemName;
            HostUri = hostUri;
        }

        public static RegisterApplicationMessage Create(string name, string systemName, string hostUri)
        {
            return new RegisterApplicationMessage(name, systemName, hostUri);
        }

        public string Type => nameof(RegisterApplicationMessage);

        public string Name { get; set; }

        public string SystemName { get; set; }

        public string HostUri { get; set; }
    }
}
