using System;
using System.Collections.Generic;

namespace PaladinRogue.Authentication.Application.Notifications.Send
{
    public class SendNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string NotificationType { get; set; }

        public IReadOnlyDictionary<string, string> PropertyBag { get; set; }
    }
}