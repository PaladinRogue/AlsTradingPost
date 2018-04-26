using System.Collections.Generic;

namespace Common.Api.Resources
{
    public interface ICollectionResource<T> : IResource where T : ISummaryResource
    {
        IList<T> Results { get; set; }
    }
}