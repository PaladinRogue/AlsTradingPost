using System;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency.Services.Interfaces
{
    public interface IConcurrencyService<T> where T : ICheckConcurrencyService
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
