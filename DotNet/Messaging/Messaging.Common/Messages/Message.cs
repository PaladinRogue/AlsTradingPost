using System.Threading.Tasks;
using Messaging.Common;
using Messaging.Setup.Infrastructure.Dispatchers;
using Messaging.Setup.Infrastructure.Senders;

namespace Messaging.Setup.Infrastructure
{
    public static class Message
    {
        private static volatile IMessageSender _messageSender;

        private static IMessageSender MessageSender
        {
            get => _messageSender;
            set => _messageSender = value;
        }

        public static void SetMessageSender(this IMessageSender messageSender)
        {
            if (MessageSender == null)
            {
                MessageSender = messageSender;
            }
        }

        public static Task SendAsync<T>(T message) where T : IMessage
        {
            if (MessageSender == null)
            {
                throw new MessageSenderNotSetException();
            }

            return MessageSender.SendAsync(message);
        }
    }
}