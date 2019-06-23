using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Notifications.Emails
{
    public class LocalDevelopmentEmailNotificationSender : IEmailNotificationSender
    {
        private readonly ILogger<LocalDevelopmentEmailNotificationSender> _logger;
        private readonly string _devInboxPath;

        public LocalDevelopmentEmailNotificationSender(ILogger<LocalDevelopmentEmailNotificationSender> logger)
        {
            _logger = logger;
            _devInboxPath = Path.Combine(Directory.GetCurrentDirectory(), "LocalEmailInbox", "Dev");
            Directory.CreateDirectory(_devInboxPath);
        }

        public void Send(SendEmailNotificationAdto sendEmailNotificationAdto)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(sendEmailNotificationAdto.From);
                    message.Body = sendEmailNotificationAdto.HtmlBody;
                    message.Subject = sendEmailNotificationAdto.Subject;
                    message.IsBodyHtml = true;
                    sendEmailNotificationAdto.Recipients.ToList().ForEach(message.To.Add);
                    using (SmtpClient smtpClient = new SmtpClient("developmentHost"))
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = _devInboxPath;
                        smtpClient.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to send local dev email.", ex);
                throw;
            }
        }
    }
}