using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Notifications.Send;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.ApplicationServices.Subscribers
{
    public class SendNotificationMessageSubscriber : MessageSubscriber<SendNotificationMessage, SendNotificationMessageSubscriber>
    {
        private readonly ISendNotificationKernalService _sendNotificationKernalService;

        private readonly ILogger<SendNotificationMessageSubscriber> _logger;

        public SendNotificationMessageSubscriber(
            IMessageBus messageBus,
            ISendNotificationKernalService sendNotificationKernalService,
            ILogger<SendNotificationMessageSubscriber> logger) : base(messageBus)
        {
            _logger = logger;
            _sendNotificationKernalService = sendNotificationKernalService;
        }

        public override async Task HandleAsync(SendNotificationMessage message)
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
                _logger.LogCritical(e, "Unable to create admin user");
            }
        }
    }
}