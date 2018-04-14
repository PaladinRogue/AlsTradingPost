using System;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Services.Interfaces
{
    public interface IConcurrencyQueryService<T>
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
