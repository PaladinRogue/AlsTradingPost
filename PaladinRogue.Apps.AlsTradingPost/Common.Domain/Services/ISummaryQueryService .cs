using System.Collections.Generic;

namespace Common.Domain.Services
{
    public interface ISummaryQueryService<T>
    {
        IList<T> GetAll();
    }
}
