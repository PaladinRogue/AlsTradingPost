using System;
using System.Reflection;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class ConcurrencyDomainException : DomainException
    {
        public ConcurrencyDomainException(IVersionedEntity entity, Exception innerException)
            : base(_formatConcurrencyException(entity.GetType(), entity.Id, entity.Version), innerException)
        {
        }

        public ConcurrencyDomainException()
            : base("No concurrency token has been provided")
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

        private static string _formatConcurrencyException(MemberInfo type, Guid id, int version)
        {
            return $"Concurrency check failed for entity: { type.Name } with Id: { id } and Version: { version.ToString() }";
        }
    }
}
