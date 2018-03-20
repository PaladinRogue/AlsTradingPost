using System.Collections.Generic;

namespace Common.Domain.Interfaces
{
    public interface ISummaryQueryService<T>
    {
        IList<T> GetAll();
    }
}
