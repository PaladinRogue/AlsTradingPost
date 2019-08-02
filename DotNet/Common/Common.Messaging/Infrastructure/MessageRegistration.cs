using System;
using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure
{
    public class MessageRegistration
    {
        private MessageRegistration(Type handlerType, Delegate asyncHandler)
        {
            HandlerType = handlerType;
            AsyncHandler = asyncHandler;
        }

        public Type HandlerType { get; }

        public Delegate AsyncHandler { get; }

        public static MessageRegistration Create<T>(Type handlerType, Func<T, Task> asyncHandler)
        {
            return new MessageRegistration(handlerType, asyncHandler);
        }
    }
}
