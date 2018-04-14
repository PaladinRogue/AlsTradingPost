﻿using System;
using System.Reflection;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Exceptions
{
    public class DeleteDomainException : DomainException
    {
        public DeleteDomainException(IEntity entity, Exception innerException)
            : base(_formatDeleteException(entity.GetType(), entity.Id, entity.Version), innerException)
        {
        }

        public DeleteDomainException(MemberInfo type, Guid id, IConcurrencyVersion version)
            : base(_formatDeleteException(type, id, version.Version))
        {
        }

        public DeleteDomainException(MemberInfo type, Guid id, IConcurrencyVersion version, Exception innerException)
            : base(_formatDeleteException(type, id, version.Version), innerException)
        {
        }

        private static string _formatDeleteException(MemberInfo type, Guid id, byte[] version)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(version);
            }

            return $"Failed to delete entity: { type.Name } with Id: { id } and Version: { BitConverter.ToInt32(version, 0) }";
        }
    }
}
