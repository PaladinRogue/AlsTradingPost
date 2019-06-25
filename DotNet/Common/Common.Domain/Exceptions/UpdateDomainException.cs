using System;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Exceptions
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
