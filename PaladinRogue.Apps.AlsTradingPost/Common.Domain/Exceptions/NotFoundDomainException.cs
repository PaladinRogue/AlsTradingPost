using System;

namespace Common.Domain.Exceptions
{
    public class NotFoundDomainException : DomainException {
	    public NotFoundDomainException(string message) : base(message)
	    {
	    }

	    public NotFoundDomainException(string message, Exception innerException) : base(message, innerException)
	    {
	    }
    }
}
