using System;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class NotFoundDomainException : DomainException {
	    public NotFoundDomainException(string message) : base(message)
	    {
	    }

	    public NotFoundDomainException(string message, Exception innerException) : base(message, innerException)
	    {
	    }
    }
}
