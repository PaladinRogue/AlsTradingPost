﻿using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities.Exceptions
{
    public class InvalidPasswordDomainException : DomainException
    {
        public InvalidPasswordDomainException()
        {
        }

        public InvalidPasswordDomainException(string message) : base(message)
        {
        }

        public InvalidPasswordDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}