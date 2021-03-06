﻿using System;

namespace PaladinRogue.Library.Core.Domain.DataProtectors
{
    public class DataProtectorNotSetException : Exception
    {

        public DataProtectorNotSetException()
        {
        }

        public DataProtectorNotSetException(string message) : base(message)
        {
        }

        public DataProtectorNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}