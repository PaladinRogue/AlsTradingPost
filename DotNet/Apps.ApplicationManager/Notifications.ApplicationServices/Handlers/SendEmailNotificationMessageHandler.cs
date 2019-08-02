using System;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;
using Notifications.ApplicationServices.Emails;
using Notifications.ApplicationServices.Emails.Send;

namespace Notifications.ApplicationServices.Handlers
{
    public class SendEmailNotificationMessageHandler : MessageHandler<SendEmailNotificationMessage, SendEmailNotificationMessageHandler>
    {
        private readonly ISendEmailNotificationKernalService _sendEmailNotificationKernalService;

        private readonly ILogger<SendEmailNotificationMessageHandler> _logger;

        public SendEmailNotificationMessageHandler(
            IMessageBus messageBus,
            ISendEmailNotificationKernalService sendEmailNotificationKernalService,
            ILogger<SendEmailNotificationMessageHandler> logger) : base(messageBus)
        {
            _logger = logger;
            _sendEmailNotificationKernalService = sendEmailNotificationKernalService;
        }

        public override async Task ExecuteAsync(SendEmailNotificationMessage message)
        {
            try
            {
                await _sendEmailNotificationKernalService.ExecuteAsync(new SendEmailNotificationAdto
                {
                    Sender = message.Sender,
                    Recipients = message.Recipients,
                    Subject = message.Subject,
                    HtmlBody = message.HtmlBody
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to send email", message);
            }
        }
    }
}