using System;
using System.Linq;
using PaladinRogue.Library.Core.Common.Extensions;

namespace PaladinRogue.Authentication.Application.Notifications.Emails
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

            string htmlBody = buildEmailAdto.EmailTemplate.Format(buildEmailAdto.PropertyBag.ToDictionary(p => p.Key, p => p.Value));

            return new EmailAdto
            {
                Subject = buildEmailAdto.Subject,
                HtmlBody = htmlBody
            };
        }
    }
}