﻿using System;

namespace PaladinRogue.Library.Messaging.Common.Senders
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
