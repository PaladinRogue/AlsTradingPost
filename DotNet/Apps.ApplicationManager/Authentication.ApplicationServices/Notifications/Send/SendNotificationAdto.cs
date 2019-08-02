using System;
using System.Collections.Generic;

namespace Notifications.ApplicationServices.Emails
{
    public class SendNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string NotificationType { get; set; }

        public IReadOnlyDictionary<string, string> PropertyBag { get; set; }
    }
}