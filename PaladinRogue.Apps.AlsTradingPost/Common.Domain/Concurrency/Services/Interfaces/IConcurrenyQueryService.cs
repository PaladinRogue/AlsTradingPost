using System;
using Common.Domain.Services.Query;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Services.Interfaces
{
    public interface IConcurrencyQueryService<T> where T : ICheckConcurrencyService
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
