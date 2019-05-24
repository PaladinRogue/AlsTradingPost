using System;
using Common.Resources.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency.Services.Interfaces
{
    public interface IConcurrencyService<T> where T : ICheckConcurrencyService
    {
        void CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
