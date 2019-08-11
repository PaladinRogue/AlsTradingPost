using System;
using System.Threading.Tasks;
using PaladinRogue.Libray.Core.Domain.Aggregates;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Owners
{
    public interface IResourceOwnerProvider
    {
        Type Type { get; }

        Task<IAggregateOwner> GetOwnerAsync(Guid resourceId);
    }
}
