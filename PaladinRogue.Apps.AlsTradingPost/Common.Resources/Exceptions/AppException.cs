using System;

namespace Common.Resources.Exceptions
{
    public class AppException : Exception
    {
        public ExceptionType Type { get; set; }

        public AppException(ExceptionType type, string message) : base(message)
        {
            Type = type;
        }

        public AppException(ExceptionType statusCode, Exception inner) : this(statusCode, inner.ToString()) { }
    }
}