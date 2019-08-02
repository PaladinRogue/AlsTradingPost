using System.Collections.Generic;

namespace Common.Api.Resources
{
    public interface ICollectionResource<T> : IResource where T : IResource
    {
        IList<T> Results { get; set; }
    }
}