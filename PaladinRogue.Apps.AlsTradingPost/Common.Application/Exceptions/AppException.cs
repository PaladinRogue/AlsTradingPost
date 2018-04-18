using System;

namespace Common.Application.Exceptions
{
    public class AppException : Exception
    {
        public ExceptionType Type { get; set; }
        
        public AppException(ExceptionType type, Exception inner) : this(type, null, inner) {}

        public AppException(ExceptionType type, string message) : this(type, message, null) {}

        public AppException(ExceptionType type, string message, Exception inner) : base(message, inner)
        {
            Type = type;
        }
    }
}