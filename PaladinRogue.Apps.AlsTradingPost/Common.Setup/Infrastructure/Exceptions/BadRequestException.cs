using System;

namespace Common.Setup.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }
        
        public BadRequestException(string message) : base(message)
        {
        }
        
        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
