using System;

namespace Common.Messaging.Infrastructure
{
    public class MessageSubscription
    {
        private MessageSubscription(Type handlerType, Delegate handler)
        {
            HandlerType = handlerType;
            Handler = handler;
        }

        public Type HandlerType { get; set; }

        public Delegate Handler { get; set; }

        public static MessageSubscription Create(Type handlerType, Delegate handler)
        {
            return new MessageSubscription(handlerType, handler);
        }
    }
}
