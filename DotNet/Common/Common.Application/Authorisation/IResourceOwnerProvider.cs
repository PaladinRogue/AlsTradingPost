using System;
using Common.Domain.Models.Interfaces;

namespace Common.Application.Authorisation
{
    public interface IResourceOwnerProvider
    {
        IAggregateOwner GetOwner(Type resourceType, Guid resourceId);
    }
}
