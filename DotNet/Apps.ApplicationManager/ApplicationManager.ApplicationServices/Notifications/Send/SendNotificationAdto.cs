using System;
using System.Collections.Generic;

namespace ApplicationManager.ApplicationServices.Notifications.Send
{
    public class SendNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string NotificationType { get; set; }

        public IReadOnlyDictionary<string, string> PropertyBag { get; set; }
    }
}