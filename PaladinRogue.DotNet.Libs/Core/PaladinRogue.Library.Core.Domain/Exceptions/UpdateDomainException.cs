using System;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.Core.Domain.Exceptions
{
    [Serializable]
    public class UpdateDomainException : DomainException
    {
        public UpdateDomainException(IVersionedEntity entity, Exception innerException)
            : base($"Unable to update entity {entity}", innerException)
        {
        }
    }
}
