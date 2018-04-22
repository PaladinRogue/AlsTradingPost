using System;

namespace Common.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ExceptionType Type { get; }
        
        public ApplicationException(ExceptionType type, Exception inner) : this(type, null, inner) {}

        public ApplicationException(ExceptionType type, string message) : this(type, message, null) {}

        public ApplicationException(ExceptionType type, string message, Exception inner) : base(message, inner)
        {
            Type = type;
        }
    }
}