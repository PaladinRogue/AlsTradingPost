using System;
using Common.Domain.Aggregates;

namespace Common.Authorisation
{
    public interface IResourceOwnerProvider
    {
        IAggregateOwner GetOwner(Type resourceType, Guid resourceId);
    }
}
