﻿using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities.Exceptions
{
    public class InvalidTwoFactorTokenDomainException : DomainException
    {
        public InvalidTwoFactorTokenDomainException()
        {
        }

        public InvalidTwoFactorTokenDomainException(string message) : base(message)
        {
        }

        public InvalidTwoFactorTokenDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}