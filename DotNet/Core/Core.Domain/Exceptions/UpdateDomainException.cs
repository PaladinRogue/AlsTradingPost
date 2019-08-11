using System;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Domain.Exceptions
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
