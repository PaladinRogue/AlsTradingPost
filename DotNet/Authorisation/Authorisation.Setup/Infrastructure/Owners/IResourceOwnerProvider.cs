using System;
using System.Threading.Tasks;
using Common.Domain.Aggregates;

namespace Authorisation.Application
{
    public interface IResourceOwnerProvider
    {
        Type Type { get; }

        Task<IAggregateOwner> GetOwnerAsync(Guid resourceId);
    }
}
