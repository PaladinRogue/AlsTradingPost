using System.Collections.Generic;

namespace AlsTradingPost.Domain.Interfaces
{
    public interface ISummaryQueryService<T>
    {
        IList<T> GetAll();
    }
}
