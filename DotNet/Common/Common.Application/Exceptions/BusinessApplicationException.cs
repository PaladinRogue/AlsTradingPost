using System;

namespace Common.Application.Exceptions
{
    public class BusinessApplicationException : Exception
    {
        public ExceptionType Type { get; }
        
        public BusinessApplicationException(ExceptionType type, Exception inner) : this(type, null, inner) {}

        public BusinessApplicationException(ExceptionType type, string message) : this(type, message, null) {}

        public BusinessApplicationException(ExceptionType type, string message, Exception inner) : base(message, inner)
        {
            Type = type;
        }
    }
}