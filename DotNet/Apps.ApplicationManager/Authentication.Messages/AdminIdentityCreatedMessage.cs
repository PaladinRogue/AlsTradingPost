using System;
using Messaging.Common;

namespace Authentication.Messages
{
    public class AdminIdentityCreatedMessage : IMessage
    {
        protected AdminIdentityCreatedMessage()
        {
        }

        protected AdminIdentityCreatedMessage(string applicationName, Guid identityId)
        {
            IdentityId = identityId;
            ApplicationName = applicationName;
        }

        public static AdminIdentityCreatedMessage Create(string applicationName, Guid identityId)
        {
            return new AdminIdentityCreatedMessage(applicationName, identityId);
        }

        public string Type => nameof(AdminIdentityCreatedMessage);

        public string ApplicationName { get; set; }

        public Guid IdentityId { get; set; }
    }
}
