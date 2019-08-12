using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.Aggregates;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners
{
    public interface IResourceOwnerProvider
    {
        Type Type { get; }

        Task<IAggregateOwner> GetOwnerAsync(Guid resourceId);
    }
}
