﻿using System;
using System.Collections.Generic;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Authentication.Messages
{
    public class SendNotificationMessage : IMessage
    {
        protected SendNotificationMessage()
        {
        }

        protected SendNotificationMessage(string notificationType, Guid identityId, IReadOnlyDictionary<string, string> propertyBag)
        {
            IdentityId = identityId;
            NotificationType = notificationType;
            PropertyBag = propertyBag;
        }

        public static SendNotificationMessage Create(string notificationType, Guid identityId, IReadOnlyDictionary<string, string> propertyBag)
        {
            return new SendNotificationMessage(notificationType, identityId, propertyBag);
        }

        public string Type => nameof(SendNotificationMessage);

        public Guid IdentityId { get; set; }

        public string NotificationType { get; set; }

        public IReadOnlyDictionary<string, string> PropertyBag { get; set; }
    }
}
