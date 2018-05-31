using System;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Services.Query
{
    public interface ICheckConcurrencyQueryService
    {
        bool CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
