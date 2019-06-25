using System;

namespace Common.Domain.Models.DataProtection
{
    public class DataHasherNotSetException : Exception
    {

        public DataHasherNotSetException()
        {
        }

        public DataHasherNotSetException(string message) : base(message)
        {
        }

        public DataHasherNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}