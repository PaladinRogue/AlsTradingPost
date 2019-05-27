using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Messages
{
    public class CreateTwoFactorAuthenticationIdentityMessage : IMessage
    {
        public string EmailAddress { get; set; }
    }
}
