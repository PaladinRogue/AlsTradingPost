using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Notifications.Send;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;
using Notifications.ApplicationServices.Emails;

namespace Authentication.ApplicationServices.Handlers
{
    public class SendNotificationMessageHandler : MessageHandler<SendNotificationMessage, SendNotificationMessageHandler>
    {
        private readonly ISendNotificationKernalService _sendNotificationKernalService;

        private readonly ILogger<SendNotificationMessageHandler> _logger;

        public SendNotificationMessageHandler(
            IMessageBus messageBus,
            ISendNotificationKernalService sendNotificationKernalService,
            ILogger<SendNotificationMessageHandler> logger) : base(messageBus)
        {
            _logger = logger;
            _sendNotificationKernalService = sendNotificationKernalService;
        }

        public override async Task ExecuteAsync(SendNotificationMessage message)
        {
            try
            {
                await _sendNotificationKernalService.SendAsync(new SendNotificationAdto
                {
                    IdentityId = message.IdentityId,
                    NotificationType = message.NotificationType,
                    PropertyBag = message.PropertyBag
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to send notification", message);
            }
        }
    }
}