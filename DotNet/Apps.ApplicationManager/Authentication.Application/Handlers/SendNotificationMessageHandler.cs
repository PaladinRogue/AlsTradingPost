using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaladinRogue.Authentication.Application.Notifications.Send;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Libray.Messaging.Common.Handlers;
using PaladinRogue.Libray.Messaging.Common.MessageBus;

namespace PaladinRogue.Authentication.Application.Handlers
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