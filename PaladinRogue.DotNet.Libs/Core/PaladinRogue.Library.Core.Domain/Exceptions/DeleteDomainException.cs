using System;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.Core.Domain.Exceptions
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
