using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Api.Concurrency.Interfaces
{
    public interface IVersionedResource : IVersioned<IConcurrencyVersion>, IResource
    {
    }
}
