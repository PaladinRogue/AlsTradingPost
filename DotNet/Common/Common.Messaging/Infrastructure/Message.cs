using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Senders;

namespace Common.Messaging.Infrastructure
{
    public class Message
    {
        private static volatile IMessageSender _messageSender;

        protected Message()
        {
        }

        protected static IMessageSender MessageSender
        {
            get => _messageSender;
            set => _messageSender = value;
        }

        public static void SetMessageSender(IMessageSender messageSender)
        {
            if (MessageSender == null)
            {
                MessageSender = messageSender;
            }
        }

        public static void Send<T>(T message) where T : IMessage
        {
            if (MessageSender == null)
            {
                throw new MessageSenderNotSetException();
            }

            MessageSender.Send(message);
        }
    }
}