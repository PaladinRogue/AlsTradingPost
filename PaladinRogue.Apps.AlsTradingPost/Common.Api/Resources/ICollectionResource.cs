using System.Collections.Generic;

namespace Common.Api.Resources
{
    public interface ICollectionResource<T> where T : ISummaryResource
    {
        IList<T> Results { get; set; }
    }
}