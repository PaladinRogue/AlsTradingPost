using System;
using System.Threading.Tasks;
using Authentication.Application.Notifications.Send;
using Messaging.Messages;
using Messaging.Setup.Infrastructure.Handlers;
using Messaging.Setup.Infrastructure.MessageBus;
using Microsoft.Extensions.Logging;
using Notifications.ApplicationServices.Emails;

namespace Authentication.Application.Handlers
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