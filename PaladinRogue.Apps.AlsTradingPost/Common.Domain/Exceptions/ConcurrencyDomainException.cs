using System;
using System.Reflection;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Exceptions
{
    public class ConcurrencyDomainException : DomainException
    {
        public ConcurrencyDomainException(IEntity entity, Exception innerException)
            : base(_formatConcurrencyException(entity.GetType(), entity.Id, entity.Version), innerException)
        {
            
        }

        public ConcurrencyDomainException(MemberInfo type, Guid id, IConcurrencyVersion version)
            : base(_formatConcurrencyException(type, id, version.Version))
        {
        }

        public ConcurrencyDomainException(MemberInfo type, Guid id, IConcurrencyVersion version, Exception innerException)
            : base(_formatConcurrencyException(type, id, version.Version), innerException)
        {
        }

        private static string _formatConcurrencyException(MemberInfo type, Guid id, byte[] version)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(version);
            }

            return $"Concurrency check failed for entity: { type.Name } with Id: { id } and Version: { BitConverter.ToInt32(version, 0) }";
        }
    }
}
