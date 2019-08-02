using System;

namespace Common.Messaging.Infrastructure.Dispatchers
{
    [Serializable]
    public class MessageSenderNotSetException : Exception
    {
        public MessageSenderNotSetException()
        {
        }

        public MessageSenderNotSetException(string message) : base(message)
        {
        }

        public MessageSenderNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
