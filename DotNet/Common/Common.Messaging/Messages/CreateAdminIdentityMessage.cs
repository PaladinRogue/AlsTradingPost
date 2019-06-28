using System;
using System.Collections.Generic;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Messages
{
    public class CreateAdminIdentityMessage : IMessage
    {
        protected CreateAdminIdentityMessage()
        {
        }

        protected CreateAdminIdentityMessage(string applicationName, Guid identityId)
        {
            IdentityId = identityId;
            ApplicationName = applicationName;
        }

        public static CreateAdminIdentityMessage Create(string applicationName, Guid identityId)
        {
            return new CreateAdminIdentityMessage(applicationName, identityId);
        }

        public string Type => nameof(CreateAdminIdentityMessage);

        public string ApplicationName { get; set; }

        public Guid IdentityId { get; set; }
    }
}
