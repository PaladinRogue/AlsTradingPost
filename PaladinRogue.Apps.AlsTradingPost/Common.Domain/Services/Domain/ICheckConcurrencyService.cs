using System;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Services.Domain
{
    public interface ICheckConcurrencyService
    {
        bool CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
