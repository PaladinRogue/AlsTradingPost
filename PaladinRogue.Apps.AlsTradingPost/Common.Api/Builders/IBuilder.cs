using System.Collections.Generic;

namespace Common.Api.Builders
{
    public interface IBuilder<TKey, TValue>
    {
        IDictionary<TKey, TValue> Build();
    }
}