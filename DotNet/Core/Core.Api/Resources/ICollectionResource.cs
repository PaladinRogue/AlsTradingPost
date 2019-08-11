using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Api.Resources
{
    public interface ICollectionResource<T> : IResource where T : IResource
    {
        IList<T> Results { get; set; }
    }
}