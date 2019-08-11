using System;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Domain.Exceptions
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
