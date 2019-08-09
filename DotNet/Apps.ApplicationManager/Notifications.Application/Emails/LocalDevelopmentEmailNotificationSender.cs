using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Notifications.Application.Emails
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

        public async Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(sendEmailNotificationAdto.Sender);
                    message.Body = sendEmailNotificationAdto.HtmlBody;
                    message.Subject = sendEmailNotificationAdto.Subject;
                    message.IsBodyHtml = true;
                    sendEmailNotificationAdto.Recipients.ToList().ForEach(message.To.Add);
                    using (SmtpClient smtpClient = new SmtpClient("developmentHost"))
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = _devInboxPath;
                        await smtpClient.SendMailAsync(message);
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