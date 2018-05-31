using System;
using Common.Domain.Services.Domain;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Services.Interfaces
{
    public interface IConcurrencyService<T> where T : ICheckConcurrencyService
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
