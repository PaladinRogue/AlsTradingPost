using System;

namespace Common.Domain.Models.DataProtection
{
    public class HashFactoryNotSetException : Exception
    {

        public HashFactoryNotSetException()
        {
        }

        public HashFactoryNotSetException(string message) : base(message)
        {
        }

        public HashFactoryNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}