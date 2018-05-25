using System;
using System.Reflection;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class CreateDomainException : DomainException
    {
        public CreateDomainException(IEntity entity, Exception innerException)
            : base(_formatCreateException(entity.GetType(), entity.Id), innerException)
        {
        }
        public CreateDomainException(IVersionedEntity entity, Exception innerException)
            : base(_formatCreateException(entity.GetType(), entity.Id), innerException)
        {
        }

        public CreateDomainException(MemberInfo type, Guid id)
            : base(_formatCreateException(type, id))
        {
        }

        public CreateDomainException(MemberInfo type, Guid id, Exception innerException)
            : base(_formatCreateException(type, id), innerException)
        {
        }

        private static string _formatCreateException(MemberInfo type, Guid id)
        {
            return $"Failed to create entity: { type.Name } with Id: { id }";
        }
    }
}
