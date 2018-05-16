using System;

namespace Common.Domain.DomainEvents
{
    public class PendingDomainEventDirectorNotSetException : Exception
    {
        
        public PendingDomainEventDirectorNotSetException()
        {
        }
        
        public PendingDomainEventDirectorNotSetException(string message) : base(message)
        {
        }
        
        public PendingDomainEventDirectorNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}