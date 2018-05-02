﻿using System;

namespace Common.Domain.Exceptions
{
    public class GetByIdDomainException : DomainException
    {
        public GetByIdDomainException(Guid id)
            : base(_formatGetByIdException(id))
        {
        }

        public GetByIdDomainException(Guid id, Exception innerException)
            : base(_formatGetByIdException(id), innerException)
        {
        }

        private static string _formatGetByIdException(Guid id)
        {
            return $"Failed to get entity with Id: { id }";
        }
    }
}
