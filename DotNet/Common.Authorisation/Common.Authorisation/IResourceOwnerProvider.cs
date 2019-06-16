using System;
using Common.Domain.Models.Interfaces;

namespace Common.Authorisation
{
    public interface IResourceOwnerProvider
    {
        IAggregateOwner GetOwner(Type resourceType, Guid resourceId);
    }
}
