using System;

namespace Message.Broker.Messages
{
    public class Subscription
    {
        private Subscription(Type handlerType, Delegate handler)
        {
            HandlerType = handlerType;
            Handler = handler;
        }

        public Type HandlerType { get; set; }

        public Delegate Handler { get; set; }

        public static Subscription Create(Type handlerType, Delegate handler)
        {
            return new Subscription(handlerType, handler);
        }
    }
}
