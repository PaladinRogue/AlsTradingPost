using ApplicationManager.ApplicationServices.Notifications.Send;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;

namespace ApplicationManager.ApplicationServices.Subscribers
{
    public class SendNotificationMessageSubscriber : MessageSubscriber<SendNotificationMessage, SendNotificationMessageSubscriber>
    {
        private readonly ISendNotificationKernalService _sendNotificationKernalService;

        public SendNotificationMessageSubscriber(
            IMessageBus messageBus,
            ISendNotificationKernalService sendNotificationKernalService) : base(messageBus)
        {
            _sendNotificationKernalService = sendNotificationKernalService;
        }

        public override void Handle(SendNotificationMessage message)
        {
            _sendNotificationKernalService.Send(new SendNotificationAdto
            {
                IdentityId = message.IdentityId,
                NotificationType = message.NotificationType,
                PropertyBag = message.PropertyBag
            });
        }
    }
}