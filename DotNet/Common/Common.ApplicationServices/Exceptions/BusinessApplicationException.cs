using System;

namespace Common.ApplicationServices.Exceptions
{
    public class BusinessApplicationException : Exception
    {
        public ExceptionType Type { get; }

        public string Code { get; }

        public BusinessApplicationException(ExceptionType type, Exception inner) : this(type, null, null, inner) {}

        public BusinessApplicationException(ExceptionType type, string message = null) : this(type, null, message, null) {}

        public BusinessApplicationException(ExceptionType type, string message, Exception inner) : this(type, null, message, inner) {}

        public BusinessApplicationException(ExceptionType type, string code, string message) : this(type, code, message, null) {}

        public BusinessApplicationException(ExceptionType type, string code, string message, Exception inner) : base(message, inner)
        {
            Type = type;
            Code = code;
        }
    }
}