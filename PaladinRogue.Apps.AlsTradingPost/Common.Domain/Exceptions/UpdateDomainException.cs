using System;
using System.Reflection;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Exceptions
{
    public class UpdateDomainException : DomainException
    {
        public UpdateDomainException(IVersionedEntity entity, Exception innerException)
            : base(_formatUpdateException(entity.GetType(), entity.Id, entity.Version), innerException)
        {
        }

        public UpdateDomainException(MemberInfo type, Guid id, IConcurrencyVersion version)
            : base(_formatUpdateException(type, id, version.Version))
        {
        }

        public UpdateDomainException(MemberInfo type, Guid id, IConcurrencyVersion version, Exception innerException)
            : base(_formatUpdateException(type, id, version.Version), innerException)
        {
        }

        private static string _formatUpdateException(MemberInfo type, Guid id, byte[] version)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(version);
            }

            return $"Failed to update entity: { type.Name } with Id: { id } and Version: { BitConverter.ToInt32(version, 0) }";
        }
    }
}
