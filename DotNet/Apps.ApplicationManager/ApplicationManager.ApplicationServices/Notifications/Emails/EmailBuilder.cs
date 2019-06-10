using System;

namespace ApplicationManager.ApplicationServices.Notifications.Emails
{
    public class EmailBuilder : IEmailBuilder
    {
        public EmailAdto Build(BuildEmailAdto buildEmailAdto)
        {
            if (buildEmailAdto == null)
            {
                throw new ArgumentNullException(nameof(buildEmailAdto));
            }
            
            if (buildEmailAdto.EmailTemplate == null)
            {
                throw new ArgumentNullException(nameof(buildEmailAdto.EmailTemplate));
            }
            
            string htmlBody = buildEmailAdto.EmailTemplate;
            
            foreach ((string key, string value) in buildEmailAdto.PropertyBag)
            {
                htmlBody = htmlBody.Replace($"{{{{{key}}}}}", value, StringComparison.OrdinalIgnoreCase);
            }
            
            return new EmailAdto
            {
                Subject = buildEmailAdto.Subject,
                HtmlBody = htmlBody
            };
        }
    }
}