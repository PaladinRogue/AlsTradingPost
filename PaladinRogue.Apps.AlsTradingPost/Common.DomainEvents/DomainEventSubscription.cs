using System;

namespace DomainEvent.Broker
{
    public class DomainEventSubscription
    {
        private DomainEventSubscription(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public Type HandlerType { get; set; }

        public Delegate Handler { get; set; }

        public static DomainEventSubscription Create(Type handlerType)
        {
            return new DomainEventSubscription(handlerType);
        }
    }
}
