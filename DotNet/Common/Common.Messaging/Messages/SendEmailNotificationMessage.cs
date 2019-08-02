﻿using System.Collections.Generic;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Messages
{
    public class SendEmailNotificationMessage : IMessage
    {
        protected SendEmailNotificationMessage()
        {
        }

        protected SendEmailNotificationMessage(string sender, IEnumerable<string> recipients, string subject, string htmlBody)
        {
            Sender = sender;
            Recipients = recipients ;
            Subject = subject;
            HtmlBody = htmlBody;
        }

        public static SendEmailNotificationMessage Create(string sender, string recipient, string subject, string htmlBody)
        {
            return Create(sender, new List<string> {recipient}, subject, htmlBody);
        }

        public static SendEmailNotificationMessage Create(string sender, IEnumerable<string> recipients, string subject, string htmlBody)
        {
            return new SendEmailNotificationMessage(sender, recipients, subject, htmlBody);
        }

        public string Type => nameof(SendEmailNotificationMessage);

        public string Sender { get; set; }

        public IEnumerable<string> Recipients { get; set; }

        public string Subject { get; set; }

        public string HtmlBody { get; set; }
    }
}
