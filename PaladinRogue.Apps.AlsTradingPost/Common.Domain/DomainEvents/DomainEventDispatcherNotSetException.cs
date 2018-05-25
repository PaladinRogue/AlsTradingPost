﻿using System;

namespace Common.Domain.DomainEvents
{
    [Serializable]
    public class DomainEventDispatcherNotSetException : Exception
    {
        public DomainEventDispatcherNotSetException()
        {
        }

        public DomainEventDispatcherNotSetException(string message) : base(message)
        {
        }

        public DomainEventDispatcherNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}