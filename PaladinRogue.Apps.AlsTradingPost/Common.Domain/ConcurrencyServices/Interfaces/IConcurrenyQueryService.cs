using System;
using Common.Resources.Concurrency;

namespace Common.Domain.ConcurrencyServices.Interfaces
{
    public interface IConcurrencyQueryService<T>
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
