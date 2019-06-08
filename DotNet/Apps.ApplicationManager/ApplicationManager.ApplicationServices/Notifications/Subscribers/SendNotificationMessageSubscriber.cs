using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Common.Messaging.Subscribers;

namespace ApplicationManager.ApplicationServices.Notifications.Subscribers
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