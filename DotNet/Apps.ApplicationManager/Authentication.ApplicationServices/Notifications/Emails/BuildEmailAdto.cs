using System.Collections.Generic;

namespace Authentication.ApplicationServices.Notifications.Emails
{
    public class BuildEmailAdto
    {
        public string EmailTemplate { get; set; }
        
        public string Subject { get; set; }
        
        public IReadOnlyDictionary<string, string> PropertyBag { get; set; }
    }
}