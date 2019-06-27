using System;
using Common.Domain.Entities;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class DeleteDomainException : DomainException
    {
        public DeleteDomainException(IVersionedEntity entity, Exception innerException)
            : base($"Unable to delete entity {entity}", innerException)
        {
        }
    }
}
