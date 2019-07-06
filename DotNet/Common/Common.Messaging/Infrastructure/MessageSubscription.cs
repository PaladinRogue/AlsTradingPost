using System;
using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure
{
    public class MessageSubscription
    {
        private MessageSubscription(Type handlerType, Delegate asyncHandler)
        {
            HandlerType = handlerType;
            AsyncHandler = asyncHandler;
        }

        public Type HandlerType { get; }

        public Delegate AsyncHandler { get; }

        public static MessageSubscription Create<T>(Type handlerType, Func<T, Task> asyncHandler)
        {
            return new MessageSubscription(handlerType, asyncHandler);
        }
    }
}
