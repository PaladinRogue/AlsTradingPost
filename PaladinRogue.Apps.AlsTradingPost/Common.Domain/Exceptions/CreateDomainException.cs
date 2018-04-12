using System;
using System.Reflection;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Domain.Exceptions
{
    public class CreateDomainException : DomainException
    {
        public CreateDomainException(IEntity entity, Exception innerException)
            : base(_formatCreateException(entity.GetType(), entity.Id, entity.Version), innerException)
        {
        }

        public CreateDomainException(MemberInfo type, Guid id, IConcurrencyVersion version)
            : base(_formatCreateException(type, id, version.Version))
        {
        }

        public CreateDomainException(MemberInfo type, Guid id, IConcurrencyVersion version, Exception innerException)
            : base(_formatCreateException(type, id, version.Version), innerException)
        {
        }

        private static string _formatCreateException(MemberInfo type, Guid id, byte[] version)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(version);
            }

            return $"Failed to create entity: { type.Name } with Id: { id } and Version: { BitConverter.ToInt32(version, 0) }";
        }
    }
}
