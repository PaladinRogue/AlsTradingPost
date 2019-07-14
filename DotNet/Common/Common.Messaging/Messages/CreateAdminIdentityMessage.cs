using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Messages
{
    public class CreateAdminIdentityMessage : IMessage
    {
        protected CreateAdminIdentityMessage()
        {
        }

        protected CreateAdminIdentityMessage(string applicationName, string emailAddress)
        {
            ApplicationName = applicationName;
            EmailAddress = emailAddress;
        }

        public static CreateAdminIdentityMessage Create(string applicationName, string emailAddress)
        {
            return new CreateAdminIdentityMessage(applicationName, emailAddress);
        }

        public string Type => nameof(CreateAdminIdentityMessage);

        public string ApplicationName { get; set; }

        public string EmailAddress { get; set; }
    }
}
