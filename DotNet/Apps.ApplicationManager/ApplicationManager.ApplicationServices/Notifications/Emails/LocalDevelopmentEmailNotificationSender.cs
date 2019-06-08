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
            _devInboxPath = Path.Combine("C:\\Projects\\PaladinRogue", "LocalEmailInbox", "Dev");
            Directory.CreateDirectory(this._devInboxPath);
        }

        public void Send(SendEmailNotificationAdto sendEmailNotificationAdto)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(sendEmailNotificationAdto.From);
                    message.Body = sendEmailNotificationAdto.HtmlBody;
                    message.IsBodyHtml = true;
                    sendEmailNotificationAdto.Recipients.ToList<string>().ForEach(new Action<string>(message.To.Add));
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