using System.Collections.Generic;

namespace Authentication.ApplicationServices.Notifications.Emails
{
    public class SendEmailNotificationAdto
    {
        public string From { get; set; }
        
        public IEnumerable<string> Recipients { get; set; }

        public string HtmlBody { get; set; }

        public string Subject { get; set; }
    }
}