using System;

namespace Common.Domain.Models.PasswordProtection
{
    public class PasswordProtectorNotSetException : Exception
    {

        public PasswordProtectorNotSetException()
        {
        }

        public PasswordProtectorNotSetException(string message) : base(message)
        {
        }

        public PasswordProtectorNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}