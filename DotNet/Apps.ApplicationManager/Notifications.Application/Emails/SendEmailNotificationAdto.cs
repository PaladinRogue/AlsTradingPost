using System.Collections.Generic;

namespace Notifications.Application.Emails
{
    public class SendEmailNotificationAdto
    {
        public string Sender { get; set; }

        public IEnumerable<string> Recipients { get; set; }

        public string HtmlBody { get; set; }

        public string Subject { get; set; }
    }
}